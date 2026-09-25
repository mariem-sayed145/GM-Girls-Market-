import { useEffect, useState } from 'react'
import { getMyRequests, createRequest } from '../api/requestApi'

function CustomerRequests() {
    const [requests, setRequests] = useState([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        const load = async () => {
            try {
                setLoading(true)
                setError('')

                const data = await getMyRequests()

                setRequests(data)
            } catch (err) {
                setError(err.message || 'Failed to load requests.')
            } finally {
                setLoading(false)
            }
        }

        load()
    }, [])

    return (
        <div className="admin-page">
            <div className="admin-header">
                <div>
                    <span className="admin-label">DASHBOARD</span>
                    <h1>Your Requests</h1>
                    <p>View your submitted service requests and their statuses.</p>
                </div>
            </div>

            {loading && <div className="admin-message">Loading requests...</div>}

            {!loading && error && (
                <div className="admin-message error">{error}</div>
            )}

            {!loading && !error && requests.length === 0 && (
                <div className="admin-empty">
                    <h2>No Requests Yet</h2>
                    <p>Submit a request from your dashboard.</p>
                </div>
            )}

            {!loading && !error && requests.length > 0 && (
                <div className="admin-product-grid">
                    {requests.map((r) => (
                        <div className="admin-product-card" key={r.id}>
                            <div className="admin-product-info">
                                <div>
                                    <h3>Service #{r.serviceId}</h3>
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

export default CustomerRequests
