const API_URL = 'http://localhost:5262/api/Products'

const getAuthHeaders = () => {
    const token = localStorage.getItem('gm_token')

    return token
        ? {
            Authorization: `Bearer ${token}`,
        }
        : {}
}

export const getProducts = async (filters = {}) => {
    const params = new URLSearchParams()

    if (filters.search) params.append('search', filters.search)
    if (filters.category) params.append('category', filters.category)
    if (filters.minPrice !== undefined && filters.minPrice !== null) params.append('minPrice', String(filters.minPrice))
    if (filters.maxPrice !== undefined && filters.maxPrice !== null) params.append('maxPrice', String(filters.maxPrice))

    const url = params.toString() ? `${API_URL}?${params.toString()}` : API_URL

    const response = await fetch(url, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        throw new Error('Failed to load products.')
    }

    return response.json()
}

export const getProductById = async (id) => {
    const response = await fetch(
        `${API_URL}/${id}`,
        {
            headers: getAuthHeaders(),
        }
    )

    if (!response.ok) {
        throw new Error('Failed to load product.')
    }

    return response.json()
}

export const createProduct = async (product, image) => {
    const formData = new FormData()

    formData.append('name', product.name)
    formData.append('description', product.description)
    formData.append('price', product.price)
    formData.append('category', product.category)
    formData.append('image', image)

    const response = await fetch(API_URL, {
        method: 'POST',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        throw new Error('Failed to create product.')
    }

    return response.json()
}

export const updateProduct = async (
    id,
    product,
    image
) => {
    const formData = new FormData()

    formData.append('name', product.name)
    formData.append('description', product.description)
    formData.append('price', product.price)
    formData.append('category', product.category)

    if (image) {
        formData.append('image', image)
    }

    const response = await fetch(
        `${API_URL}/${id}`,
        {
            method: 'PUT',
            headers: getAuthHeaders(),
            body: formData,
        }
    )

    if (!response.ok) {
        throw new Error('Failed to update product.')
    }
}

export const deleteProduct = async (id) => {
    const response = await fetch(
        `${API_URL}/${id}`,
        {
            method: 'DELETE',
            headers: getAuthHeaders(),
        }
    )

    if (!response.ok) {
        throw new Error('Failed to delete product.')
    }
}