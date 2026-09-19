import { Navigate, useLocation } from 'react-router-dom'

function ProtectedRoute({ children }) {
    const location = useLocation()
    const token = localStorage.getItem('gm_token')

    if (!token) {
        return (
            <Navigate
                to="/login"
                state={{ from: location.pathname }}
                replace
            />
        )
    }

    return children
}

export default ProtectedRoute