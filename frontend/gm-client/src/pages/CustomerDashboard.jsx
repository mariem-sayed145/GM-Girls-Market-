import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { getCurrentUser, updateCurrentUser } from '../api/authApi'

function CustomerDashboard() {
    const navigate = useNavigate()
    const [user, setUser] = useState(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState(null)
    const [saving, setSaving] = useState(false)
    const [success, setSuccess] = useState(null)

    const token = localStorage.getItem('gm_token')

    const fetchUser = async () => {
        setLoading(true)
        setError(null)
        try {
            const data = await getCurrentUser(token)
            setUser(data)
        } catch (err) {
            setError(err.message || 'Failed to load user profile.')
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        fetchUser()
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const handleChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value })
    }

    const handleSubmit = async (e) => {
        e.preventDefault()
        if (!user) return
        setSaving(true)
        setSuccess(null)
        setError(null)
        try {
            await updateCurrentUser(token, {
                fullName: user.fullName,
                email: user.email,
            })

            await fetchUser()
            setSuccess('Profile updated successfully.')
        } catch (err) {
            setError(err.message || 'Failed to update profile.')
        } finally {
            setSaving(false)
        }
    }

    const handleLogout = () => {
        localStorage.removeItem('gm_token')
        localStorage.removeItem('gm_user')
        navigate('/login')
    }

    const isStaff =
        user &&
        (user.role === 1 ||
            user.role === 2 ||
            user.role === 'Employee' ||
            user.role === 'Admin')

    if (loading) {
        return (
            <div className="admin-page">
                <div className="admin-message">Loading profile...</div>
            </div>
        )
    }

    return (
        <div className="admin-page">
            <div className="admin-header">
                <div>
                    <span className="admin-label">CUSTOMER PORTAL</span>
                    <h1>Welcome, {user?.fullName || 'Customer'}</h1>
                    <p>Manage your account settings and service requests.</p>
                </div>

                <div className="admin-header-actions">
                    <Link to="/" className="secondary-btn">Browse Market</Link>
                    <Link to="/customer/requests" className="primary-btn">My Requests</Link>
                    {isStaff && (
                        <Link to="/admin/products" className="secondary-btn">Admin Panel</Link>
                    )}
                    <button type="button" className="secondary-btn" onClick={handleLogout}>
                        Log Out
                    </button>
                </div>
            </div>

            <div className="admin-form-container" style={{ maxWidth: '600px', margin: '2rem auto' }}>
                {success && (
                    <div style={{ padding: '1rem', backgroundColor: '#e6f7ec', color: '#15803d', borderRadius: '8px', marginBottom: '1.5rem', fontWeight: 500 }}>
                        ✓ {success}
                    </div>
                )}

                {error && (
                    <div className="admin-message error" style={{ marginBottom: '1.5rem' }}>
                        {error}
                    </div>
                )}

                <form className="admin-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="fullName">Full Name</label>
                        <input
                            id="fullName"
                            name="fullName"
                            type="text"
                            value={user?.fullName || ''}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="email">Email Address</label>
                        <input
                            id="email"
                            name="email"
                            type="email"
                            value={user?.email || ''}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="admin-form-actions">
                        <button
                            type="submit"
                            className="primary-btn"
                            disabled={saving}
                        >
                            {saving ? 'Saving...' : 'Update Profile'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default CustomerDashboard
