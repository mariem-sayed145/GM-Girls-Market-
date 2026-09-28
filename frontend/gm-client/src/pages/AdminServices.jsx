import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { getServices } from '../api/serviceApi'

function AdminServices() {
    const [services, setServices] = useState([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    const load = async () => {
        try {
            setIsLoading(true)
            setError('')

            const data = await getServices()

            setServices(data)
        } catch (err) {
            setError(err.message || 'Failed to load services.')
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

                    <h1>Services</h1>

                    <p>Manage your Girls Market services.</p>
                </div>

                <div className="admin-header-actions">

                    <Link to="/admin/services/add" className="primary-btn">Add Service</Link>

                </div>

            </div>

            {isLoading && <div className="admin-message">Loading services...</div>}

            {!isLoading && error && (
                <div className="admin-message error">{error}</div>
            )}

            {!isLoading && !error && services.length === 0 && (
                <div className="admin-empty">
                    <h2>No Services Yet</h2>
                    <p>Start by adding your first service.</p>

                    <Link to="/admin/services/add" className="primary-btn">Add Service</Link>
                </div>
            )}

            {!isLoading && !error && services.length > 0 && (
                <div className="admin-product-grid">
                    {services.map((s) => (
                        <div className="admin-product-card" key={s.id}>

                            <div className="admin-product-image">

                                <img src={s.imageUrl} alt={s.name} />

                                <Link to={`/admin/services/edit/${s.id}`} className="edit-btn" title="Edit service">✎</Link>

                            </div>

                            <div className="admin-product-info">

                                <div>

                                    <span className="product-category">{s.category}</span>

                                    <h3>{s.name}</h3>

                                </div>

                                <strong>{Number(s.price).toLocaleString()} EGP</strong>

                            </div>

                        </div>
                    ))}
                </div>
            )}

        </div>
    )
}

export default AdminServices
