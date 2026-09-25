import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { getAllRequests } from '../api/requestApi'

function AdminRequests() {
    const [requests, setRequests] = useState([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    const load = async () => {
        try {
            setIsLoading(true)
            setError('')

            const data = await getAllRequests()

            setRequests(data)
        } catch (err) {
            setError(err.message || 'Failed to load requests.')
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        load()
    }, [])

    return (
        <div className="admin-page">

            <div className="admin-header">

                <div>
                    <span className="admin-label">ADMIN PANEL</span>

                    <h1>Customer Requests</h1>

                    <p>Manage customer service requests.</p>
                </div>

                <div className="admin-header-actions">
                    <Link to="/admin/services" className="secondary-btn">Back to Services</Link>
                </div>

            </div>

            {isLoading && <div className="admin-message">Loading requests...</div>}

            {!isLoading && error && (
                <div className="admin-message error">{error}</div>
            )}

            {!isLoading && !error && requests.length === 0 && (
                <div className="admin-empty">
                    <h2>No Requests</h2>
                    <p>There are no customer requests at the moment.</p>
                </div>
            )}

            {!isLoading && !error && requests.length > 0 && (
                <div className="admin-product-grid">
                    {requests.map((r) => (
                        <div className="admin-product-card" key={r.id}>
                            <div className="admin-product-image">
                                <Link to={`/admin/requests/edit/${r.id}`} className="edit-btn" title="Edit request">✎</Link>
                            </div>

                            <div className="admin-product-info">
                                <div>
                                    <span className="product-category">Service #{r.serviceId}</span>
                                    <h3>Request #{r.id}</h3>
                                    <p>{r.description}</p>
                                </div>

                                <strong>{r.status}</strong>
                            </div>
                        </div>
                    ))}
                </div>
            )}

        </div>
    )
}

export default AdminRequests
