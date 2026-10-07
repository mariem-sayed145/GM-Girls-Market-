import { API_BASE_URL, getAuthHeaders, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/Services`

export const getServices = async () => {
    const response = await fetch(API_URL, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load services.')
        throw new Error(message)
    }

    return response.json()
}

export const getServiceById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load service.')
        throw new Error(message)
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
        const message = await parseErrorMessage(response, 'Failed to create service.')
        throw new Error(message)
    }

    return response.json()
}

export const updateService = async (id, service, image) => {
    const formData = new FormData()

    formData.append('name', service.name)
    formData.append('description', service.description)
    formData.append('price', service.price)
    formData.append('category', service.category)

    if (image) {
        formData.append('image', image)
    }

    const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to update service.')
        throw new Error(message)
    }
}

export const deleteService = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to delete service.')
        throw new Error(message)
    }
}
