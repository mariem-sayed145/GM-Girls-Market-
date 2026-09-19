import { useState } from 'react'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { login } from '../api/authApi'

function Login() {
    const navigate = useNavigate()
    const location = useLocation()

    const [formData, setFormData] = useState({
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
            const data = await login(formData)

            localStorage.setItem(
                'gm_token',
                data.token
            )

            localStorage.setItem(
                'gm_user',
                JSON.stringify({
                    id: data.id,
                    fullName: data.fullName,
                    email: data.email,
                })
            )

            const redirectTo =
                location.state?.from ||
                '/admin/products'

            navigate(redirectTo, {
                replace: true,
            })
        } catch (error) {
            setError(
                error.message ||
                'Invalid email or password.'
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

                <h1>Welcome Back</h1>

                <p className="auth-subtitle">
                    Log in to manage your account.
                </p>

                <form
                    className="auth-form"
                    onSubmit={handleSubmit}
                >
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
                            placeholder="Your password"
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
                            ? 'Logging in...'
                            : 'Log In'}
                    </button>
                </form>

                <p className="auth-switch">
                    Don't have an account?{' '}
                    <Link to="/register">
                        Create one
                    </Link>
                </p>
            </div>
        </div>
    )
}

export default Login