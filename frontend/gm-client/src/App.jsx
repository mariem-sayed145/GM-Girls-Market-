import { BrowserRouter, Routes, Route } from 'react-router-dom'

import Home from './pages/Home'
import AdminProducts from './pages/AdminProducts'
import AddProduct from './pages/AddProduct'
import EditProduct from './pages/EditProduct'
import Login from './pages/Login'
import Register from './pages/Register'
import CustomerDashboard from './pages/CustomerDashboard'
import CustomerRequests from './pages/CustomerRequests'
import AdminRequests from './pages/AdminRequests'
import EditRequest from './pages/EditRequest'
import AdminServices from './pages/AdminServices'
import AddService from './pages/AddService'
import EditService from './pages/EditService'
import ProtectedRoute from './components/ProtectedRoute'

function App() {
    const adminOrEmployee = ['Admin', 'Employee', 1, 2]

    return (
        <BrowserRouter>
            <Routes>
                <Route
                    path="/"
                    element={<Home />}
                />

                <Route
                    path="/login"
                    element={<Login />}
                />

                <Route
                    path="/register"
                    element={<Register />}
                />

                <Route
                    path="/admin/products"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <AdminProducts />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/products/add"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <AddProduct />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/products/edit/:id"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <EditProduct />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/customer/dashboard"
                    element={
                        <ProtectedRoute>
                            <CustomerDashboard />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/customer/requests"
                    element={
                        <ProtectedRoute>
                            <CustomerRequests />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/requests"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <AdminRequests />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/requests/edit/:id"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <EditRequest />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <AdminServices />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services/add"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <AddService />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services/edit/:id"
                    element={
                        <ProtectedRoute allowedRoles={adminOrEmployee}>
                            <EditService />
                        </ProtectedRoute>
                    }
                />
            </Routes>
        </BrowserRouter>
    )
}

export default App