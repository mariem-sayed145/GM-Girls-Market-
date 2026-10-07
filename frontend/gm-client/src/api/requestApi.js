import { API_BASE_URL, getAuthHeaders, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/CustomerRequests`

export const createRequest = async (data) => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            ...getAuthHeaders(),
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to create request.')
        throw new Error(message)
    }

    return response.json()
}

export const getMyRequests = async () => {
    const response = await fetch(`${API_URL}/my`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load requests.')
        throw new Error(message)
    }

    return response.json()
}

export const getRequestByIdAdmin = async (id) => {
    const response = await fetch(`${API_URL}/admin/${id}`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load request.')
        throw new Error(message)
    }

    return response.json()
}

export const getAllRequests = async () => {
    const response = await fetch(API_URL, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load requests.')
        throw new Error(message)
    }

    return response.json()
}

export const updateRequestStatus = async (id, status) => {
    const response = await fetch(`${API_URL}/${id}/status`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            ...getAuthHeaders(),
        },
        body: JSON.stringify(status),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to update status.')
        throw new Error(message)
    }
}
