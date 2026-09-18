import { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import {
    deleteProduct,
    getProductById,
    updateProduct,
} from '../api/productApi'

function EditProduct() {
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
        const loadProduct = async () => {
            try {
                setIsLoading(true)
                setError('')

                const product = await getProductById(id)

                setFormData({
                    name: product.name,
                    description: product.description,
                    price: product.price,
                    category: product.category,
                    imageUrl: product.imageUrl,
                })
            } catch (error) {
                setError(
                    error.message ||
                    'Failed to load product.'
                )
            } finally {
                setIsLoading(false)
            }
        }

        loadProduct()
    }, [id])

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        })
    }

    const handleImageChange = (e) => {
        const selectedImage = e.target.files?.[0] || null
        setImage(selectedImage)
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        setIsSaving(true)
        setError('')

        try {
            await updateProduct(
                id,
                {
                    name: formData.name,
                    description: formData.description,
                    price: formData.price,
                    category: formData.category,
                },
                image
            )

            navigate('/admin/products')
        } catch (error) {
            setError(
                error.message ||
                'Failed to update product.'
            )
        } finally {
            setIsSaving(false)
        }
    }

    const handleDelete = async () => {
        const confirmed = window.confirm(
            'Are you sure you want to delete this product?'
        )

        if (!confirmed) {
            return
        }

        setIsDeleting(true)
        setError('')

        try {
            await deleteProduct(id)
            navigate('/admin/products')
        } catch (error) {
            setError(
                error.message ||
                'Failed to delete product.'
            )
        } finally {
            setIsDeleting(false)
        }
    }

    if (isLoading) {
        return (
            <div className="admin-page">
                <div className="admin-message">
                    Loading product...
                </div>
            </div>
        )
    }

    if (error && !formData.name) {
        return (
            <div className="admin-page">
                <div className="admin-message error">
                    {error}
                </div>

                <Link
                    to="/admin/products"
                    className="secondary-btn"
                >
                    Back to Products
                </Link>
            </div>
        )
    }

    return (
        <div className="admin-page">
            <div className="admin-form-header">
                <div>
                    <span className="admin-label">
                        ADMIN PANEL
                    </span>

                    <h1>Edit Product</h1>

                    <p>
                        Update your product information.
                    </p>
                </div>

                <Link
                    to="/admin/products"
                    className="secondary-btn"
                >
                    Back to Products
                </Link>
            </div>

            <form
                className="admin-form"
                onSubmit={handleSubmit}
            >
                <div className="form-group">
                    <label htmlFor="name">
                        Product Name
                    </label>

                    <input
                        id="name"
                        name="name"
                        type="text"
                        value={formData.name}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="description">
                        Description
                    </label>

                    <textarea
                        id="description"
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        rows="5"
                        required
                    />
                </div>

                <div className="admin-form-row">
                    <div className="form-group">
                        <label htmlFor="price">
                            Price
                        </label>

                        <input
                            id="price"
                            name="price"
                            type="number"
                            min="0"
                            step="0.01"
                            value={formData.price}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="category">
                            Category
                        </label>

                        <input
                            id="category"
                            name="category"
                            type="text"
                            value={formData.category}
                            onChange={handleChange}
                            required
                        />
                    </div>
                </div>

                <div className="form-group">
                    <label>
                        Current Image
                    </label>

                    {formData.imageUrl && (
                        <img
                            src={formData.imageUrl}
                            alt={formData.name}
                            className="edit-product-preview"
                        />
                    )}
                </div>

                <div className="form-group">
                    <label htmlFor="image">
                        Replace Image
                    </label>

                    <input
                        id="image"
                        name="image"
                        type="file"
                        accept="image/*"
                        onChange={handleImageChange}
                    />

                    {image && (
                        <p className="selected-file">
                            Selected: {image.name}
                        </p>
                    )}
                </div>

                {error && (
                    <p className="admin-form-error">
                        {error}
                    </p>
                )}

                <div className="admin-form-actions">
                    <button
                        type="button"
                        className="delete-btn"
                        onClick={handleDelete}
                        disabled={
                            isDeleting ||
                            isSaving
                        }
                    >
                        {isDeleting
                            ? 'Deleting...'
                            : 'Delete Product'}
                    </button>

                    <div className="admin-form-right-actions">
                        <Link
                            to="/admin/products"
                            className="secondary-btn"
                        >
                            Cancel
                        </Link>

                        <button
                            type="submit"
                            className="primary-btn"
                            disabled={
                                isSaving ||
                                isDeleting
                            }
                        >
                            {isSaving
                                ? 'Saving...'
                                : 'Save Changes'}
                        </button>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default EditProduct
