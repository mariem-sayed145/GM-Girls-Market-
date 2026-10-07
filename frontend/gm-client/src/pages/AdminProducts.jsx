import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { getProducts } from '../api/productApi'
import { getImageUrl } from '../api/config'

function AdminProducts() {
    const navigate = useNavigate()
    const [products, setProducts] = useState([])
    const [isLoading, setIsLoading] = useState(true)
    const [error, setError] = useState('')

    const loadProducts = async () => {
        try {
            setIsLoading(true)
            setError('')
            const data = await getProducts()
            setProducts(data)
        } catch (err) {
            setError(err.message || 'Failed to load products.')
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        loadProducts()
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
                    <h1>Products</h1>
                    <p>Manage your Girls Market products.</p>
                </div>

                <div className="admin-header-actions">
                    <Link to="/" className="secondary-btn">View Store</Link>
                    <Link to="/admin/services" className="secondary-btn">Services</Link>
                    <Link to="/admin/requests" className="secondary-btn">Requests</Link>
                    <Link to="/admin/products/add" className="primary-btn">Add Product</Link>
                    <button type="button" className="secondary-btn" onClick={handleLogout}>
                        Log Out
                    </button>
                </div>
            </div>

            <div className="admin-products">
                {isLoading && <div className="admin-message">Loading products...</div>}

                {!isLoading && error && (
                    <div className="admin-message error">{error}</div>
                )}

                {!isLoading && !error && products.length === 0 && (
                    <div className="admin-empty">
                        <h2>No Products Yet</h2>
                        <p>Start by adding your first product.</p>
                        <Link to="/admin/products/add" className="primary-btn">Add Product</Link>
                    </div>
                )}

                {!isLoading && !error && products.length > 0 && (
                    <div className="admin-product-grid">
                        {products.map((product) => (
                            <div className="admin-product-card" key={product.id}>
                                <div className="admin-product-image">
                                    <img
                                        src={getImageUrl(product.imageUrl)}
                                        alt={product.name}
                                    />

                                    <Link
                                        to={`/admin/products/edit/${product.id}`}
                                        className="edit-btn"
                                        aria-label={`Edit ${product.name}`}
                                        title="Edit product"
                                    >
                                        ✎
                                    </Link>
                                </div>

                                <div className="admin-product-info">
                                    <div>
                                        <span className="product-category">
                                            {product.category}
                                        </span>
                                        <h3>{product.name}</h3>
                                    </div>

                                    <strong>
                                        {Number(product.price).toLocaleString()} EGP
                                    </strong>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    )
}

export default AdminProducts
