import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { createService } from '../api/serviceApi'

function AddService() {
    const navigate = useNavigate()

    const [formData, setFormData] = useState({
        name: '',
        description: '',
        price: '',
        category: '',
    })

    const [image, setImage] = useState(null)
    const [isSubmitting, setIsSubmitting] = useState(false)
    const [error, setError] = useState('')

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value })
    }

    const handleImageChange = (e) => {
        const selectedImage = e.target.files?.[0] || null
        setImage(selectedImage)
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        if (!image) {
            setError('Please select a service image.')
            return
        }

        setIsSubmitting(true)
        setError('')

        try {
            await createService(
                {
                    name: formData.name,
                    description: formData.description,
                    price: formData.price,
                    category: formData.category,
                },
                image
            )

            navigate('/admin/services')
        } catch (err) {
            setError(err.message || 'Failed to create service.')
        } finally {
            setIsSubmitting(false)
        }
    }

    return (
        <div className="admin-page">
            <div className="admin-form-header">
                <div>
                    <span className="admin-label">ADMIN PANEL</span>

                    <h1>Add Service</h1>

                    <p>Add a new service to Girls Market.</p>
                </div>

                <Link to="/admin/services" className="secondary-btn">Back to Services</Link>
            </div>

            <form className="admin-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="name">Service Name</label>

                    <input id="name" name="name" type="text" value={formData.name} onChange={handleChange} placeholder="e.g. Personal Styling" required />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Description</label>

                    <textarea id="description" name="description" value={formData.description} onChange={handleChange} placeholder="Describe the service..." rows="5" required />
                </div>

                <div className="admin-form-row">
                    <div className="form-group">
                        <label htmlFor="price">Price</label>

                        <input id="price" name="price" type="number" min="0" step="0.01" value={formData.price} onChange={handleChange} placeholder="1000" required />
                    </div>

                    <div className="form-group">
                        <label htmlFor="category">Category</label>

                        <input id="category" name="category" type="text" value={formData.category} onChange={handleChange} placeholder="Wellness" required />
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="image">Service Image</label>

                    <input id="image" name="image" type="file" accept="image/*" onChange={handleImageChange} required />

                    {image && <p className="selected-file">Selected: {image.name}</p>}
                </div>

                {error && <p className="admin-form-error">{error}</p>}

                <div className="admin-form-actions">
                    <Link to="/admin/services" className="secondary-btn">Cancel</Link>

                    <button type="submit" className="primary-btn" disabled={isSubmitting}>{isSubmitting ? 'Adding...' : 'Add Service'}</button>
                </div>
            </form>
        </div>
    )
}

export default AddService
