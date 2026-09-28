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
                        <ProtectedRoute>
                            <AdminProducts />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/products/add"
                    element={
                        <ProtectedRoute>
                            <AddProduct />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/products/edit/:id"
                    element={
                        <ProtectedRoute>
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
                        <ProtectedRoute>
                            <AdminRequests />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/requests/edit/:id"
                    element={
                        <ProtectedRoute>
                            <EditRequest />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services"
                    element={
                        <ProtectedRoute>
                            <AdminServices />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services/add"
                    element={
                        <ProtectedRoute>
                            <AddService />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/services/edit/:id"
                    element={
                        <ProtectedRoute>
                            <EditService />
                        </ProtectedRoute>
                    }
                />
            </Routes>
        </BrowserRouter>
    )
}

export default App