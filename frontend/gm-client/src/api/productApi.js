import { API_BASE_URL, getAuthHeaders, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/Products`

export const getProducts = async (filters = {}) => {
    const params = new URLSearchParams()

    if (filters.search) params.append('search', filters.search)
    if (filters.category) params.append('category', filters.category)
    if (filters.minPrice !== undefined && filters.minPrice !== null && filters.minPrice !== '') {
        params.append('minPrice', String(filters.minPrice))
    }
    if (filters.maxPrice !== undefined && filters.maxPrice !== null && filters.maxPrice !== '') {
        params.append('maxPrice', String(filters.maxPrice))
    }

    const url = params.toString() ? `${API_URL}?${params.toString()}` : API_URL

    const response = await fetch(url, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load products.')
        throw new Error(message)
    }

    return response.json()
}

export const getProductById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load product.')
        throw new Error(message)
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
        const message = await parseErrorMessage(response, 'Failed to create product.')
        throw new Error(message)
    }

    return response.json()
}

export const updateProduct = async (id, product, image) => {
    const formData = new FormData()

    formData.append('name', product.name)
    formData.append('description', product.description)
    formData.append('price', product.price)
    formData.append('category', product.category)

    if (image) {
        formData.append('image', image)
    }

    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to update product.')
        throw new Error(message)
    }
}

export const deleteProduct = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to delete product.')
        throw new Error(message)
    }
}