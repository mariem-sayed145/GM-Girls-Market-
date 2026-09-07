import './App.css'

function App() {
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

    return (
        <div className="app">

            {/* NAVBAR */}
            <nav className="navbar">
                <div className="logo">
                    <img src="/images/logo.jpg" alt="GM Logo" />
                    <span>GM</span>
                </div>

                <div className="nav-links">
                    <a href="#home">Home</a>
                    <a href="#categories">Categories</a>
                    <a href="#products">Best Sellers</a>
                    <a href="#about">About</a>
                </div>

                <button className="nav-btn">
                    Start Selling
                </button>
            </nav>


            {/* HERO */}
            <section className="hero" id="home">

                <div className="hero-content">

                    <span className="hero-tag">
                        ✦ Made for girls, by girls
                    </span>

                    <h1>
                        Discover
                        <span> Something </span>
                        You'll Love.
                    </h1>

                    <p>
                        Discover unique fashion, accessories and handmade
                        products from talented women around you.
                    </p>

                    <div className="hero-buttons">
                        <button className="primary-btn">
                            Explore Products 
                        </button>

                        <button className="secondary-btn">
                            Start Selling
                        </button>
                    </div>

                </div>

                <div className="hero-image">

                    <div className="circle"></div>

                    <img
                        src="/images/logo.jpg"
                        alt="GM"
                    />

                    <div className="floating-card card-one">
                        ✨ New Collection
                    </div>

                    <div className="floating-card card-two">
                        ♡ Best Seller
                    </div>

                </div>

            </section>


            {/* CATEGORIES */}
            <section className="categories" id="categories">

                <div className="section-title">
                    <span>EXPLORE</span>
                    <h2>Shop by Category</h2>
                    <p>Find something that matches your style.</p>
                </div>

                <div className="category-grid">

                    <div className="category-card">
                        
                        <h3>Fashion</h3>
                    </div>

                    <div className="category-card">
                     
                        <h3>Accessories</h3>
                    </div>

                    <div className="category-card">
                        
                        <h3>Bags</h3>
                    </div>

                    <div className="category-card">
                        
                        <h3>Gifts</h3>
                    </div>

                </div>

            </section>


            {/* BEST SELLERS */}
            <section className="products" id="products">

                <div className="section-title">
                    <span>OUR FAVORITES</span>
                    <h2>Best Sellers</h2>
                    <p>The pieces everyone is loving right now.</p>
                </div>

                <div className="product-grid">

                    {products.map((product) => (
                        <div className="product-card" key={product.name}>

                            <div className="product-image">
                                <img
                                    src={product.image}
                                    alt={product.name}
                                />

                                <span className="heart">
                                    ♡
                                </span>

                            </div>

                            <div className="product-info">
                                <h3>{product.name}</h3>
                                <p>{product.price}</p>
                            </div>

                        </div>
                    ))}

                </div>

            </section>


            {/* CTA */}
            <section className="cta">

                <div>
                    <span>YOUR STYLE. YOUR BUSINESS.</span>

                    <h2>
                        Have something
                        <br />
                        beautiful to sell?
                    </h2>

                    <p>
                        Join GM and turn your passion into a business.
                    </p>

                    <button className="primary-btn">
                        Start Selling
                    </button>
                </div>

            </section>


            {/* FOOTER */}
            <footer>
                <div className="footer-logo">
                    <img src="/images/logo.jpg" alt="GM" />
                    <strong>GM</strong>
                </div>

                <p>
                    © 2026 Girls Market. Made with love.
                </p>
            </footer>

        </div>
    )
}

export default App