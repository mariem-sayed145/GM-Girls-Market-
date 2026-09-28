const API_URL = 'http://localhost:5262/api/Services'

const getAuthHeaders = () => {
    const token = localStorage.getItem('gm_token')

    return token
        ? {
            Authorization: `Bearer ${token}`,
        }
        : {}
}

export const getServices = async (filters = {}) => {
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
        throw new Error('Failed to load services.')
    }

    return response.json()
}

export const getServiceById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, { headers: getAuthHeaders() })

    if (!response.ok) {
        throw new Error('Failed to load service.')
    }

    return response.json()
}

export const createService = async (service, image) => {
    const formData = new FormData()

    formData.append('name', service.name)
    formData.append('description', service.description)
    formData.append('price', service.price)
    formData.append('category', service.category)
    formData.append('image', image)

    const response = await fetch(API_URL, {
        method: 'POST',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        const message = await response.text()
        throw new Error(message || 'Failed to create service.')
    }

    return response.json()
}

export const updateService = async (id, service, image) => {
    const formData = new FormData()

    formData.append('name', service.name)
    formData.append('description', service.description)
    formData.append('price', service.price)
    formData.append('category', service.category)

    if (image) formData.append('image', image)

    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        const message = await response.text()
        throw new Error(message || 'Failed to update service.')
    }
}

export const deleteService = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        throw new Error('Failed to delete service.')
    }
}
