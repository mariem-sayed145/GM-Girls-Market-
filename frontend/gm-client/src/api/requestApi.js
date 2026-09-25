const API_URL = 'http://localhost:5262/api/CustomerRequests'

const getAuthHeaders = () => {
    const token = localStorage.getItem('gm_token')

    return token
        ? {
            Authorization: `Bearer ${token}`,
        }
        : {}
}

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
        const message = await response.text()
        throw new Error(message || 'Failed to create request.')
    }

    return response.json()
}

export const getMyRequests = async () => {
    const response = await fetch(`${API_URL}/my`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        throw new Error('Failed to load requests.')
    }

    return response.json()
}

export const getRequestByIdAdmin = async (id) => {
    const response = await fetch(`${API_URL}/admin/${id}`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        throw new Error('Failed to load request.')
    }

    return response.json()
}

export const getAllRequests = async () => {
    const response = await fetch(API_URL, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        throw new Error('Failed to load requests.')
    }

    return response.json()
}

export const updateRequestStatus = async (id, status) => {
    const response = await fetch(
        `${API_URL}/${id}/status`,
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                ...getAuthHeaders(),
            },
            body: JSON.stringify(status),
        }
    )

    if (!response.ok) {
        const message = await response.text()
        throw new Error(message || 'Failed to update status.')
    }
}
