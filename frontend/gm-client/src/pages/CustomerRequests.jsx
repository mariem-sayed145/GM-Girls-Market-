import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { getMyRequests, createRequest } from '../api/requestApi'
import { getServices } from '../api/serviceApi'

function CustomerRequests() {
    const [requests, setRequests] = useState([])
    const [services, setServices] = useState([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    // New Request Form
    const [showForm, setShowForm] = useState(false)
    const [submitting, setSubmitting] = useState(false)
    const [formSuccess, setFormSuccess] = useState('')
    const [formError, setFormError] = useState('')
    const [formData, setFormData] = useState({
        serviceId: '',
        description: '',
        preferredDate: '',
    })

    const loadData = async () => {
        try {
            setLoading(true)
            setError('')

            const [reqs, servs] = await Promise.all([
                getMyRequests(),
                getServices().catch(() => []),
            ])

            setRequests(reqs)
            setServices(servs)
        } catch (err) {
            setError(err.message || 'Failed to load requests.')
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        let isMounted = true

        Promise.all([
            getMyRequests(),
            getServices().catch(() => []),
        ])
            .then(([reqs, servs]) => {
                if (isMounted) {
                    setRequests(reqs)
                    setServices(servs)
                    setError('')
                }
            })
            .catch((err) => {
                if (isMounted) {
                    setError(err.message || 'Failed to load requests.')
                }
            })
            .finally(() => {
                if (isMounted) {
                    setLoading(false)
                }
            })

        return () => {
            isMounted = false
        }
    }, [])

    const handleFormChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        })
    }

    const handleCreateRequest = async (e) => {
        e.preventDefault()
        setSubmitting(true)
        setFormError('')
        setFormSuccess('')

        try {
            await createRequest({
                serviceId: Number(formData.serviceId),
                description: formData.description,
                preferredDate: formData.preferredDate ? new Date(formData.preferredDate).toISOString() : null,
            })

            setFormSuccess('Request submitted successfully!')
            setFormData({ serviceId: '', description: '', preferredDate: '' })
            setShowForm(false)
            await loadData()
        } catch (err) {
            setFormError(err.message || 'Failed to submit request.')
        } finally {
            setSubmitting(false)
        }
    }

    const getServiceName = (serviceId) => {
        const found = services.find((s) => s.id === serviceId)
        return found ? found.name : `Service #${serviceId}`
    }

    return (
        <div className="admin-page">
            <div className="admin-header">
                <div>
                    <span className="admin-label">CUSTOMER PORTAL</span>
                    <h1>Your Service Requests</h1>
                    <p>Track your submitted service inquiries and submit new requests.</p>
                </div>

                <div className="admin-header-actions">
                    <Link to="/customer/dashboard" className="secondary-btn">My Profile</Link>
                    <button
                        type="button"
                        className="primary-btn"
                        onClick={() => setShowForm(!showForm)}
                    >
                        {showForm ? 'Close Form' : '+ New Request'}
                    </button>
                </div>
            </div>

            {formSuccess && (
                <div style={{ padding: '1rem', backgroundColor: '#e6f7ec', color: '#15803d', borderRadius: '8px', marginBottom: '1.5rem', fontWeight: 500 }}>
                    ✓ {formSuccess}
                </div>
            )}

            {showForm && (
                <div style={{ background: '#fff', padding: '1.5rem', borderRadius: '12px', border: '1px solid #e5e7eb', marginBottom: '2rem' }}>
                    <h2 style={{ marginBottom: '1rem', fontSize: '1.25rem' }}>Submit a Service Request</h2>

                    {formError && (
                        <div className="admin-message error" style={{ marginBottom: '1rem' }}>
                            {formError}
                        </div>
                    )}

                    <form className="admin-form" onSubmit={handleCreateRequest}>
                        <div className="form-group">
                            <label htmlFor="serviceId">Select Service</label>
                            <select
                                id="serviceId"
                                name="serviceId"
                                value={formData.serviceId}
                                onChange={handleFormChange}
                                required
                            >
                                <option value="">-- Choose a Service --</option>
                                {services.map((s) => (
                                    <option key={s.id} value={s.id}>
                                        {s.name} ({Number(s.price).toLocaleString()} EGP)
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className="form-group">
                            <label htmlFor="description">What do you need?</label>
                            <textarea
                                id="description"
                                name="description"
                                placeholder="Describe your request in detail..."
                                value={formData.description}
                                onChange={handleFormChange}
                                rows="4"
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="preferredDate">Preferred Date (Optional)</label>
                            <input
                                id="preferredDate"
                                name="preferredDate"
                                type="date"
                                value={formData.preferredDate}
                                onChange={handleFormChange}
                            />
                        </div>

                        <div className="admin-form-actions">
                            <button
                                type="button"
                                className="secondary-btn"
                                onClick={() => setShowForm(false)}
                            >
                                Cancel
                            </button>
                            <button
                                type="submit"
                                className="primary-btn"
                                disabled={submitting}
                            >
                                {submitting ? 'Submitting...' : 'Submit Request'}
                            </button>
                        </div>
                    </form>
                </div>
            )}

            {loading && <div className="admin-message">Loading your requests...</div>}

            {!loading && error && (
                <div className="admin-message error">{error}</div>
            )}

            {!loading && !error && requests.length === 0 && (
                <div className="admin-empty">
                    <h2>No Requests Yet</h2>
                    <p>Have something customized or crafted? Submit your first request above!</p>
                </div>
            )}

            {!loading && !error && requests.length > 0 && (
                <div className="admin-product-grid">
                    {requests.map((r) => (
                        <div className="admin-product-card" key={r.id}>
                            <div className="admin-product-info">
                                <div>
                                    <span className="product-category">{getServiceName(r.serviceId)}</span>
                                    <h3>Request #{r.id}</h3>
                                    <p>{r.description}</p>
                                    {r.preferredDate && (
                                        <small style={{ color: '#888', display: 'block', marginTop: '0.25rem' }}>
                                            Preferred Date: {new Date(r.preferredDate).toLocaleDateString()}
                                        </small>
                                    )}
                                </div>

                                <div>
                                    <span className={`status-pill status-${String(r.status).toLowerCase()}`}>
                                        {r.status}
                                    </span>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    )
}

export default CustomerRequests
