
import { useState } from 'react'
import './App.css'

function App() {

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


    const products = [
        {
            name: 'Classic Blazer',
            price: '1,250 EGP',
            image: '/images/Classic Blazer.jpg',
        },
        {
            name: 'Minimal Necklace',
            price: '450 EGP',
            image: '/images/Minimal Necklace.jpg',
        },
        {
            name: 'Everyday Bag',
            price: '850 EGP',
            image: '/images/Everyday Bag.jpg',
        },
    ]


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

            const response = await fetch(
                'http://localhost:5262/api/Contact',
                {
                    method: 'POST',

                    headers: {
                        'Content-Type': 'application/json',
                    },

                    body: JSON.stringify(formData),
                }
            )

            const data = await response.json()


            if (!response.ok) {

                throw new Error(
                    data.message || 'Something went wrong.'
                )
            }


            setStatus({
                type: 'success',
                message: 'Your message has been sent successfully.',
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
                message:
                    error.message ||
                    'Failed to send your message.',
            })

        } finally {

            setIsSubmitting(false)

        }
    }


    return (
        <div className="app">


            {/* =========================
                NAVBAR
            ========================= */}

            <nav className="navbar">

                <div className="logo">

                    <img
                        src="/images/logo.jpg"
                        alt="GM Logo"
                    />

                    <span>GM</span>

                </div>


                <div className="nav-links">

                    <a href="#home">
                        Home
                    </a>

                    <a href="#categories">
                        Categories
                    </a>

                    <a href="#products">
                        Best Sellers
                    </a>

                    <a href="#contact">
                        Contact
                    </a>

                </div>


                <button
                    className="nav-btn"
                    onClick={() =>
                        document
                            .getElementById('contact')
                            ?.scrollIntoView({
                                behavior: 'smooth',
                            })
                    }
                >
                    Start Selling
                </button>

            </nav>


            {/* =========================
                HERO
            ========================= */}

            <section
                className="hero"
                id="home"
            >

                <div className="hero-content">

                    <span className="hero-tag">
                        Made for girls, by girls
                    </span>


                    <h1>
                        Discover
                        <span> Something </span>
                        You'll Love.
                    </h1>


                    <p>
                        Discover unique fashion, accessories
                        and handmade products from talented
                        women around you.
                    </p>


                    <div className="hero-buttons">

                        <a
                            href="#products"
                            className="primary-btn"
                        >
                            Explore Products
                        </a>


                        <a
                            href="#contact"
                            className="secondary-btn"
                        >
                            Start Selling
                        </a>

                    </div>

                </div>


                <div className="hero-image">

                    <div className="circle"></div>


                    <img
                        src="/images/logo.jpg"
                        alt="GM"
                    />


                    <div className="floating-card card-one">
                        New Collection
                    </div>


                    <div className="floating-card card-two">
                        Best Seller
                    </div>

                </div>

            </section>


            {/* =========================
                CATEGORIES
            ========================= */}

            <section
                className="categories"
                id="categories"
            >

                <div className="section-title">

                    <span>
                        EXPLORE
                    </span>

                    <h2>
                        Shop by Category
                    </h2>

                    <p>
                        Find something that matches your style.
                    </p>

                </div>


                <div className="category-grid">

                    <a
                        href="#products"
                        className="category-card"
                    >
                        <h3>
                            Fashion
                        </h3>
                    </a>


                    <a
                        href="#products"
                        className="category-card"
                    >
                        <h3>
                            Accessories
                        </h3>
                    </a>


                    <a
                        href="#products"
                        className="category-card"
                    >
                        <h3>
                            Bags
                        </h3>
                    </a>


                    <a
                        href="#products"
                        className="category-card"
                    >
                        <h3>
                            Gifts
                        </h3>
                    </a>

                </div>

            </section>


            {/* =========================
                BEST SELLERS
            ========================= */}

            <section
                className="products"
                id="products"
            >

                <div className="section-title">

                    <span>
                        OUR FAVORITES
                    </span>

                    <h2>
                        Best Sellers
                    </h2>

                    <p>
                        The pieces everyone is loving right now.
                    </p>

                </div>


                <div className="product-grid">

                    {products.map((product) => (

                        <div
                            className="product-card"
                            key={product.name}
                        >

                            <div className="product-image">

                                <img
                                    src={product.image}
                                    alt={product.name}
                                />

                            </div>


                            <div className="product-info">

                                <h3>
                                    {product.name}
                                </h3>

                                <p>
                                    {product.price}
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

                    <span>
                        YOUR STYLE. YOUR BUSINESS.
                    </span>


                    <h2>
                        Have something
                        <br />
                        beautiful to sell?
                    </h2>


                    <p>
                        Join GM and turn your passion into a business.
                    </p>


                    <button
                        className="primary-btn"
                        onClick={() =>
                            document
                                .getElementById('contact')
                                ?.scrollIntoView({
                                    behavior: 'smooth',
                                })
                        }
                    >
                        Start Selling
                    </button>

                </div>

            </section>


            {/* =========================
                CONTACT
            ========================= */}

            <section
                className="contact"
                id="contact"
            >

                <div className="section-title">

                    <span>
                        GET IN TOUCH
                    </span>

                    <h2>
                        We'd Love to Hear From You
                    </h2>

                    <p>
                        Have a question, suggestion, or just
                        want to say hello? Send us a message.
                    </p>

                </div>


                <form
                    className="contact-form"
                    onSubmit={handleSubmit}
                >

                    <div className="form-row">


                        <div className="form-group">

                            <label htmlFor="name">
                                Name
                            </label>

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

                    </div>


                    <div className="form-group">

                        <label htmlFor="subject">
                            Subject
                        </label>

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

                        <label htmlFor="message">
                            Message
                        </label>

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
                        {isSubmitting
                            ? 'Sending...'
                            : 'Send Message'}
                    </button>


                    {status.message && (

                        <p
                            className={`form - status ${ status.type } `}
                        >
                            {status.message}
                        </p>

                    )}

                </form>

            </section>


            {/* =========================
                FOOTER
            ========================= */}

            <footer>

                <div className="footer-logo">

                    <img
                        src="/images/logo.jpg"
                        alt="GM"
                    />

                    <strong>
                        GM
                    </strong>

                </div>


                <p>
                    © 2026 Girls Market. Made with love.
                </p>

            </footer>

        </div>
    )
}

export default App
