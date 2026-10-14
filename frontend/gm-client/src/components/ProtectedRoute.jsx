import { Navigate, useLocation } from 'react-router-dom'

function ProtectedRoute({ children, allowedRoles }) {
    const location = useLocation()
    const token = localStorage.getItem('gm_token')
    const userJson = localStorage.getItem('gm_user')

    if (!token) {
        return (
            <Navigate
                to="/login"
                state={{ from: location.pathname }}
                replace
            />
        )
    }

    if (allowedRoles && allowedRoles.length > 0 && userJson) {
        let isAuthorized = false
        let parseError = false

        try {
            const user = JSON.parse(userJson)
            const role = user.role

            isAuthorized = allowedRoles.some((r) => {
                if (typeof r === 'number') {
                    return r === role || (role === 'Customer' && r === 0) || (role === 'Employee' && r === 1) || (role === 'Admin' && r === 2)
                }
                if (typeof r === 'string') {
                    return (
                        r.toLowerCase() === String(role).toLowerCase() ||
                        (role === 0 && r.toLowerCase() === 'customer') ||
                        (role === 1 && r.toLowerCase() === 'employee') ||
                        (role === 2 && r.toLowerCase() === 'admin')
                    )
                }
                return false
            })
        } catch {
            parseError = true
        }

        if (parseError) {
            return <Navigate to="/login" replace />
        }

        if (!isAuthorized) {
            return <Navigate to="/customer/dashboard" replace />
        }
    }

    return children
}

export default ProtectedRoute