import { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { getServiceById, updateService, deleteService } from '../api/serviceApi'

function EditService() {
    const { id } = useParams()
    const navigate = useNavigate()

    const [formData, setFormData] = useState({
        name: '',
        description: '',
        price: '',
        category: '',
        imageUrl: '',
    })

    const [image, setImage] = useState(null)
    const [isLoading, setIsLoading] = useState(true)
    const [isSaving, setIsSaving] = useState(false)
    const [isDeleting, setIsDeleting] = useState(false)
    const [error, setError] = useState('')

    useEffect(() => {
        const load = async () => {
            try {
                setIsLoading(true)
                setError('')

                const service = await getServiceById(id)

                setFormData({
                    name: service.name,
                    description: service.description,
                    price: service.price,
                    category: service.category,
                    imageUrl: service.imageUrl,
                })
            } catch (err) {
                setError(err.message || 'Failed to load service.')
            } finally {
                setIsLoading(false)
            }
        }

        load()
    }, [id])

    const handleChange = (e) => setFormData({ ...formData, [e.target.name]: e.target.value })

    const handleImageChange = (e) => setImage(e.target.files?.[0] || null)

    const handleSubmit = async (e) => {
        e.preventDefault()

        setIsSaving(true)
        setError('')

        try {
            await updateService(id, {
                name: formData.name,
                description: formData.description,
                price: formData.price,
                category: formData.category,
            }, image)

            navigate('/admin/services')
        } catch (err) {
            setError(err.message || 'Failed to update service.')
        } finally {
            setIsSaving(false)
        }
    }

    const handleDelete = async () => {
        const confirmed = window.confirm('Are you sure you want to delete this service?')

        if (!confirmed) return

        setIsDeleting(true)
        setError('')

        try {
            await deleteService(id)
            navigate('/admin/services')
        } catch (err) {
            setError(err.message || 'Failed to delete service.')
        } finally {
            setIsDeleting(false)
        }
    }

    if (isLoading) return <div className="admin-page"><div className="admin-message">Loading service...</div></div>

    if (error && !formData.name) return <div className="admin-page"><div className="admin-message error">{error}</div><Link to="/admin/services" className="secondary-btn">Back to Services</Link></div>

    return (
        <div className="admin-page">
            <div className="admin-form-header">
                <div>
                    <span className="admin-label">ADMIN PANEL</span>
                    <h1>Edit Service</h1>
                    <p>Update your service information.</p>
                </div>

                <Link to="/admin/services" className="secondary-btn">Back to Services</Link>
            </div>

            <form className="admin-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="name">Service Name</label>
                    <input id="name" name="name" type="text" value={formData.name} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Description</label>
                    <textarea id="description" name="description" value={formData.description} onChange={handleChange} rows="5" required />
                </div>

                <div className="admin-form-row">
                    <div className="form-group">
                        <label htmlFor="price">Price</label>
                        <input id="price" name="price" type="number" min="0" step="0.01" value={formData.price} onChange={handleChange} required />
                    </div>

                    <div className="form-group">
                        <label htmlFor="category">Category</label>
                        <input id="category" name="category" type="text" value={formData.category} onChange={handleChange} required />
                    </div>
                </div>

                <div className="form-group">
                    <label>Current Image</label>
                    {formData.imageUrl && <img src={formData.imageUrl} alt={formData.name} />}
                </div>

                <div className="form-group">
                    <label htmlFor="image">Replace Image</label>
                    <input id="image" name="image" type="file" accept="image/*" onChange={handleImageChange} />
                </div>

                {error && <p className="admin-form-error">{error}</p>}

                <div className="admin-form-actions">
                    <button type="button" className="secondary-btn" onClick={() => navigate('/admin/services')}>Cancel</button>
                    <button type="submit" className="primary-btn" disabled={isSaving}>{isSaving ? 'Saving...' : 'Save'}</button>
                    <button type="button" className="secondary-btn" onClick={handleDelete} disabled={isDeleting}>{isDeleting ? 'Deleting...' : 'Delete'}</button>
                </div>
            </form>
        </div>
    )
}

export default EditService
