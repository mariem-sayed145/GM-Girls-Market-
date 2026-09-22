import { useEffect, useState } from 'react'
import { getCurrentUser, updateCurrentUser } from '../api/authApi'

function CustomerDashboard() {
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
            setError(err.message || 'Failed to load user.')
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

            // Refresh user from server
            await fetchUser()
            setSuccess('Profile updated successfully.')
        } catch (err) {
            setError(err.message || 'Failed to update profile.')
        } finally {
            setSaving(false)
        }
    }

    if (loading) {
        return <div>Loading profile...</div>
    }

    if (error) {
        return <div style={{ color: 'red' }}>{error}</div>
    }

    return (
        <div>
            <h2>Customer Dashboard</h2>

            {success && <div style={{ color: 'green' }}>{success}</div>}

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Full Name</label>
                    <input
                        name="fullName"
                        value={user.fullName}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Email</label>
                    <input
                        name="email"
                        value={user.email}
                        onChange={handleChange}
                    />
                </div>

                <button type="submit" disabled={saving}>
                    {saving ? 'Saving...' : 'Save'}
                </button>
            </form>
        </div>
    )
}

export default CustomerDashboard
