import { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { getRequestByIdAdmin, updateRequestStatus } from '../api/requestApi'

function EditRequest() {
    const { id } = useParams()
    const navigate = useNavigate()

    const [request, setRequest] = useState(null)
    const [isLoading, setIsLoading] = useState(true)
    const [isSaving, setIsSaving] = useState(false)
    const [error, setError] = useState('')
    const [status, setStatus] = useState('')

    useEffect(() => {
        const load = async () => {
            try {
                setIsLoading(true)
                setError('')

                const data = await getRequestByIdAdmin(id)

                setRequest(data)
                setStatus(data.status)
            } catch (err) {
                setError(err.message || 'Failed to load request.')
            } finally {
                setIsLoading(false)
            }
        }

        load()
    }, [id])

    const handleSubmit = async (e) => {
        e.preventDefault()

        setIsSaving(true)
        setError('')

        try {
            await updateRequestStatus(id, Number(status))
            navigate('/admin/requests')
        } catch (err) {
            setError(err.message || 'Failed to update status.')
        } finally {
            setIsSaving(false)
        }
    }

    if (isLoading) {
        return <div className="admin-page"><div className="admin-message">Loading request...</div></div>
    }

    if (error && !request) {
        return <div className="admin-page"><div className="admin-message error">{error}</div><Link to="/admin/requests" className="secondary-btn">Back</Link></div>
    }

    return (
        <div className="admin-page">
            <div className="admin-form-header">
                <div>
                    <span className="admin-label">ADMIN PANEL</span>
                    <h1>Edit Request</h1>
                    <p>Update request status.</p>
                </div>

                <Link to="/admin/requests" className="secondary-btn">Back to Requests</Link>
            </div>

            <form className="admin-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Request ID</label>
                    <input value={request.id} readOnly />
                </div>

                <div className="form-group">
                    <label>Service ID</label>
                    <input value={request.serviceId} readOnly />
                </div>

                <div className="form-group">
                    <label>Description</label>
                    <textarea value={request.description} readOnly rows={4} />
                </div>

                <div className="form-group">
                    <label>Status</label>
                    <select value={status} onChange={(e) => setStatus(e.target.value)}>
                        <option value={0}>Pending</option>
                        <option value={1}>InProgress</option>
                        <option value={2}>Completed</option>
                        <option value={3}>Cancelled</option>
                    </select>
                </div>

                {error && <p className="admin-form-error">{error}</p>}

                <div className="admin-form-actions">
                    <Link to="/admin/requests" className="secondary-btn">Cancel</Link>
                    <button type="submit" className="primary-btn" disabled={isSaving}>{isSaving ? 'Saving...' : 'Save'}</button>
                </div>
            </form>
        </div>
    )
}

export default EditRequest
