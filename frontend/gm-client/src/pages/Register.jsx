import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { register } from '../api/authApi'

function Register() {
    const navigate = useNavigate()

    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: '',
    })

    const [isSubmitting, setIsSubmitting] = useState(false)
    const [error, setError] = useState('')

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        })
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        setIsSubmitting(true)
        setError('')

        try {
            await register(formData)

            navigate('/login')
        } catch (error) {
            setError(
                error.message ||
                'Failed to create account.'
            )
        } finally {
            setIsSubmitting(false)
        }
    }

    return (
        <div className="auth-page">
            <div className="auth-card">
                <span className="admin-label">
                    GIRLS MARKET
                </span>

                <h1>Create Account</h1>

                <p className="auth-subtitle">
                    Create your account to continue.
                </p>

                <form
                    className="auth-form"
                    onSubmit={handleSubmit}
                >
                    <div className="form-group">
                        <label htmlFor="fullName">
                            Full Name
                        </label>

                        <input
                            id="fullName"
                            name="fullName"
                            type="text"
                            value={formData.fullName}
                            onChange={handleChange}
                            placeholder="Your full name"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="email">
                            Email
                        </label>

                        <input
                            id="email"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            placeholder="you@example.com"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="password">
                            Password
                        </label>

                        <input
                            id="password"
                            name="password"
                            type="password"
                            value={formData.password}
                            onChange={handleChange}
                            placeholder="At least 8 characters"
                            minLength="8"
                            required
                        />
                    </div>

                    {error && (
                        <p className="admin-form-error">
                            {error}
                        </p>
                    )}

                    <button
                        type="submit"
                        className="primary-btn auth-submit"
                        disabled={isSubmitting}
                    >
                        {isSubmitting
                            ? 'Creating Account...'
                            : 'Create Account'}
                    </button>
                </form>

                <p className="auth-switch">
                    Already have an account?{' '}
                    <Link to="/login">
                        Log in
                    </Link>
                </p>
            </div>
        </div>
    )
}

export default Register