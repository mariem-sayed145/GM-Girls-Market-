import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { getAllRequests } from '../api/requestApi'

function AdminRequests() {
    const navigate = useNavigate()
    const [requests, setRequests] = useState([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        let isMounted = true

        getAllRequests()
            .then((data) => {
                if (isMounted) {
                    setRequests(data)
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
                    setIsLoading(false)
                }
            })

        return () => {
            isMounted = false
        }
    }, [])

    const handleLogout = () => {
        localStorage.removeItem('gm_token')
        localStorage.removeItem('gm_user')
        navigate('/login')
    }

    return (
        <div className="admin-page">
            <div className="admin-header">
                <div>
                    <span className="admin-label">ADMIN PANEL</span>
                    <h1>Customer Requests</h1>
                    <p>Manage customer service requests and update their progress status.</p>
                </div>

                <div className="admin-header-actions">
                    <Link to="/" className="secondary-btn">View Store</Link>
                    <Link to="/admin/products" className="secondary-btn">Products</Link>
                    <Link to="/admin/services" className="secondary-btn">Services</Link>
                    <button type="button" className="secondary-btn" onClick={handleLogout}>
                        Log Out
                    </button>
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
                                    {r.preferredDate && (
                                        <small style={{ color: '#888' }}>
                                            Preferred: {new Date(r.preferredDate).toLocaleDateString()}
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

export default AdminRequests
