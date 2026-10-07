import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import '../App.css'
import { getProducts } from '../api/productApi'
import { getServices } from '../api/serviceApi'
import { sendContactInquiry } from '../api/contactApi'
import { getImageUrl } from '../api/config'

function Home() {
    const navigate = useNavigate()

    const [token, setToken] = useState(() => localStorage.getItem('gm_token'))
    const [user, setUser] = useState(() => {
        try {
            const raw = localStorage.getItem('gm_user')
            return raw ? JSON.parse(raw) : null
        } catch {
            return null
        }
    })

    const isStaff = user && (user.role === 1 || user.role === 2 || user.role === 'Employee' || user.role === 'Admin')

    const [formData, setFormData] = useState({
        name: '',
        email: '',
        subject: '',
        message: '',
    })

    const [status, setStatus] = useState({
        type: '',
        message: '',
    })

    const [isSubmitting, setIsSubmitting] = useState(false)

    // Products
    const [products, setProducts] = useState([])
    const [isLoadingProducts, setIsLoadingProducts] = useState(true)
    const [productsError, setProductsError] = useState('')

    // Services
    const [services, setServices] = useState([])
    const [isLoadingServices, setIsLoadingServices] = useState(true)
    const [servicesError, setServicesError] = useState('')

    // Filters
    const [search, setSearch] = useState('')
    const [categoryFilter, setCategoryFilter] = useState('')
    const [minPrice, setMinPrice] = useState('')
    const [maxPrice, setMaxPrice] = useState('')
    const [categories, setCategories] = useState([])

    /* =========================
       LOAD PRODUCTS & SERVICES
    ========================= */

    useEffect(() => {
        const loadInitialData = async () => {
            try {
                setIsLoadingProducts(true)
                setProductsError('')
                const prodData = await getProducts()
                setProducts(prodData)

                const cats = Array.from(new Set(prodData.map((p) => p.category))).filter(Boolean)
                setCategories(cats)
            } catch (error) {
                setProductsError(error.message || 'Failed to load products.')
            } finally {
                setIsLoadingProducts(false)
            }

            try {
                setIsLoadingServices(true)
                setServicesError('')
                const servData = await getServices()
                setServices(servData)
            } catch (error) {
                setServicesError(error.message || 'Failed to load services.')
            } finally {
                setIsLoadingServices(false)
            }
        }

        loadInitialData()
    }, [])

    const applyFilters = async () => {
        try {
            setIsLoadingProducts(true)
            setProductsError('')

            const filters = {
                search: search || undefined,
                category: categoryFilter || undefined,
                minPrice: minPrice ? Number(minPrice) : undefined,
                maxPrice: maxPrice ? Number(maxPrice) : undefined,
            }

            const data = await getProducts(filters)
            setProducts(data)
        } catch (error) {
            setProductsError(error.message || 'Failed to load products.')
        } finally {
            setIsLoadingProducts(false)
        }
    }

    const clearFilters = async () => {
        setSearch('')
        setCategoryFilter('')
        setMinPrice('')
        setMaxPrice('')

        try {
            setIsLoadingProducts(true)
            setProductsError('')
            const data = await getProducts()
            setProducts(data)
        } catch (error) {
            setProductsError(error.message || 'Failed to load products.')
        } finally {
            setIsLoadingProducts(false)
        }
    }

    /* =========================
       CONTACT FORM
    ========================= */

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        })
    }

    const handleSubmit = async (e) => {
        e.preventDefault()
        setIsSubmitting(true)
        setStatus({
            type: '',
            message: '',
        })

        try {
            const data = await sendContactInquiry(formData)
            setStatus({
                type: 'success',
                message: data.message || 'Your message has been sent successfully.',
            })
            setFormData({
                name: '',
                email: '',
                subject: '',
                message: '',
            })
        } catch (error) {
            setStatus({
                type: 'error',
                message: error.message || 'Failed to send your message.',
            })
        } finally {
            setIsSubmitting(false)
        }
    }

    const handleLogout = () => {
        localStorage.removeItem('gm_token')
        localStorage.removeItem('gm_user')
        setToken(null)
        setUser(null)
        navigate('/login')
    }

    return (
        <div className="app">
            {/* =========================
                NAVBAR
            ========================= */}
            <nav className="navbar">
                <div className="logo">
                    <img src="/images/logo.jpg" alt="GM Logo" />
                    <span>GM</span>
                </div>

                <div className="product-filters">
                    <input
                        type="text"
                        placeholder="Search products..."
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />

                    <select
                        value={categoryFilter}
                        onChange={(e) => setCategoryFilter(e.target.value)}
                    >
                        <option value="">All Categories</option>
                        {categories.map((c) => (
                            <option key={c} value={c}>
                                {c}
                            </option>
                        ))}
                    </select>

                    <input
                        type="number"
                        placeholder="Min Price"
                        value={minPrice}
                        onChange={(e) => setMinPrice(e.target.value)}
                    />

                    <input
                        type="number"
                        placeholder="Max Price"
                        value={maxPrice}
                        onChange={(e) => setMaxPrice(e.target.value)}
                    />

                    <button onClick={applyFilters} className="primary-btn">
                        Apply
                    </button>
                    <button onClick={clearFilters} className="secondary-btn">
                        Clear
                    </button>
                </div>

                <div className="nav-links">
                    <a href="#home">Home</a>
                    <a href="#categories">Categories</a>
                    <a href="#products">Products</a>
                    <a href="#services">Services</a>
                    <a href="#contact">Contact</a>
                </div>

                <div className="nav-auth">
                    {!token ? (
                        <>
                            <Link to="/login">Login</Link>
                            <Link to="/register">Register</Link>
                        </>
                    ) : (
                        <>
                            {isStaff ? (
                                <Link to="/admin/products">Admin Panel</Link>
                            ) : (
                                <Link to="/customer/dashboard">Dashboard</Link>
                            )}
                            <button className="nav-logout" onClick={handleLogout}>
                                Logout
                            </button>
                        </>
                    )}
                </div>

                <button
                    className="nav-btn"
                    onClick={() =>
                        document
                            .getElementById('contact')
                            ?.scrollIntoView({ behavior: 'smooth' })
                    }
                >
                    Get in Touch
                </button>
            </nav>

            {/* =========================
                HERO
            ========================= */}
            <section className="hero" id="home">
                <div className="hero-content">
                    <span className="hero-tag">Made for girls, by girls</span>

                    <h1>
                        Discover
                        <span> Something </span>
                        You'll Love.
                    </h1>

                    <p>
                        Discover unique fashion, accessories, handmade products, and services from talented women around you.
                    </p>

                    <div className="hero-buttons">
                        <a href="#products" className="primary-btn">
                            Explore Products
                        </a>
                        <a href="#services" className="secondary-btn">
                            Explore Services
                        </a>
                    </div>
                </div>

                <div className="hero-image">
                    <div className="circle"></div>
                    <img src="/images/logo.jpg" alt="GM" />
                    <div className="floating-card card-one">New Collection</div>
                    <div className="floating-card card-two">Best Seller</div>
                </div>
            </section>

            {/* =========================
                CATEGORIES
            ========================= */}
            <section className="categories" id="categories">
                <div className="section-title">
                    <span>EXPLORE</span>
                    <h2>Shop by Category</h2>
                    <p>Find something that matches your style.</p>
                </div>

                <div className="category-grid">
                    <a href="#products" className="category-card">
                        <h3>Fashion</h3>
                    </a>
                    <a href="#products" className="category-card">
                        <h3>Accessories</h3>
                    </a>
                    <a href="#products" className="category-card">
                        <h3>Bags</h3>
                    </a>
                    <a href="#products" className="category-card">
                        <h3>Gifts</h3>
                    </a>
                </div>
            </section>

            {/* =========================
                PRODUCTS / BEST SELLERS
            ========================= */}
            <section className="products" id="products">
                <div className="section-title">
                    <span>OUR FAVORITES</span>
                    <h2>Best Sellers & Products</h2>
                    <p>The pieces everyone is loving right now.</p>
                </div>

                <div className="product-grid">
                    {isLoadingProducts && (
                        <p className="products-message">Loading products...</p>
                    )}

                    {!isLoadingProducts && productsError && (
                        <p className="products-message error">{productsError}</p>
                    )}

                    {!isLoadingProducts && !productsError && products.length === 0 && (
                        <p className="products-message">No products available yet.</p>
                    )}

                    {!isLoadingProducts &&
                        !productsError &&
                        products.map((product) => (
                            <div className="product-card" key={product.id}>
                                <div className="product-image">
                                    <img
                                        src={getImageUrl(product.imageUrl)}
                                        alt={product.name}
                                    />
                                </div>

                                <div className="product-info">
                                    <span className="product-category">{product.category}</span>
                                    <h3>{product.name}</h3>
                                    <p>{Number(product.price).toLocaleString()} EGP</p>
                                </div>
                            </div>
                        ))}
                </div>
            </section>

            {/* =========================
                SERVICES SECTION
            ========================= */}
            <section className="products" id="services">
                <div className="section-title">
                    <span>OUR EXPERTISE</span>
                    <h2>Available Services</h2>
                    <p>Custom services crafted for your special needs.</p>
                </div>

                <div className="product-grid">
                    {isLoadingServices && (
                        <p className="products-message">Loading services...</p>
                    )}

                    {!isLoadingServices && servicesError && (
                        <p className="products-message error">{servicesError}</p>
                    )}

                    {!isLoadingServices && !servicesError && services.length === 0 && (
                        <p className="products-message">No services available yet.</p>
                    )}

                    {!isLoadingServices &&
                        !servicesError &&
                        services.map((service) => (
                            <div className="product-card" key={service.id}>
                                <div className="product-image">
                                    <img
                                        src={getImageUrl(service.imageUrl)}
                                        alt={service.name}
                                    />
                                </div>

                                <div className="product-info">
                                    <span className="product-category">{service.category}</span>
                                    <h3>{service.name}</h3>
                                    <p>{Number(service.price).toLocaleString()} EGP</p>
                                    <p style={{ fontSize: '0.9rem', color: '#666', marginTop: '0.25rem' }}>
                                        {service.description}
                                    </p>
                                </div>
                            </div>
                        ))}
                </div>
            </section>

            {/* =========================
                CTA
            ========================= */}
            <section className="cta">
                <div>
                    <span>YOUR STYLE. YOUR BUSINESS.</span>
                    <h2>
                        Have something
                        <br />
                        beautiful to sell?
                    </h2>
                    <p>Join GM and turn your passion into a business.</p>
                    <button
                        className="primary-btn"
                        onClick={() =>
                            document
                                .getElementById('contact')
                                ?.scrollIntoView({ behavior: 'smooth' })
                        }
                    >
                        Start Selling
                    </button>
                </div>
            </section>

            {/* =========================
                CONTACT
            ========================= */}
            <section className="contact" id="contact">
                <div className="section-title">
                    <span>GET IN TOUCH</span>
                    <h2>We'd Love to Hear From You</h2>
                    <p>
                        Have a question, suggestion, or just want to say hello? Send us a message.
                    </p>
                </div>

                <form className="contact-form" onSubmit={handleSubmit}>
                    <div className="form-row">
                        <div className="form-group">
                            <label htmlFor="name">Name</label>
                            <input
                                id="name"
                                name="name"
                                type="text"
                                value={formData.name}
                                onChange={handleChange}
                                placeholder="Your name"
                                required
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="email">Email</label>
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
                    </div>

                    <div className="form-group">
                        <label htmlFor="subject">Subject</label>
                        <input
                            id="subject"
                            name="subject"
                            type="text"
                            value={formData.subject}
                            onChange={handleChange}
                            placeholder="What is this about?"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="message">Message</label>
                        <textarea
                            id="message"
                            name="message"
                            value={formData.message}
                            onChange={handleChange}
                            placeholder="Write your message..."
                            rows="6"
                            required
                        />
                    </div>

                    <button
                        type="submit"
                        className="primary-btn contact-submit"
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Sending...' : 'Send Message'}
                    </button>

                    {status.message && (
                        <p className={`form-status ${status.type}`}>
                            {status.message}
                        </p>
                    )}
                </form>
            </section>

            {/* =========================
                FOOTER
            ========================= */}
            <footer className="footer">
                <div className="footer-logo">
                    <img src="/images/logo.jpg" alt="GM" />
                    <strong>GM</strong>
                </div>
                <p>© 2026 Girls Market. Made with love.</p>
            </footer>
        </div>
    )
}

export default Home
