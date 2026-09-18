import { BrowserRouter, Routes, Route } from 'react-router-dom'

import Home from './pages/Home'
import AdminProducts from './pages/AdminProducts'
import AddProduct from './pages/AddProduct'
import EditProduct from './pages/EditProduct'

function App() {

    return (
        <BrowserRouter>

            <Routes>

                {/* Main Website */}
                <Route
                    path="/"
                    element={<Home />}
                />


                {/* Admin Products */}
                <Route
                    path="/admin/products"
                    element={<AdminProducts />}
                />


                {/* Add Product */}
                <Route
                    path="/admin/products/add"
                    element={<AddProduct />}
                />


                {/* Edit Product */}
                <Route
                    path="/admin/products/edit/:id"
                    element={<EditProduct />}
                />

            </Routes>

        </BrowserRouter>
    )
}

export default App

