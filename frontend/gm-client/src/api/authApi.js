import { API_BASE_URL, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/Auth`

export const register = async (user) => {
    const response = await fetch(`${API_URL}/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to create account.')
        throw new Error(message)
    }

    return response.json()
}

export const login = async (credentials) => {
    const response = await fetch(`${API_URL}/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(credentials),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Invalid email or password.')
        throw new Error(message)
    }

    return response.json()
}

export const getCurrentUser = async (token) => {
    const response = await fetch(`${API_URL}/me`, {
        headers: {
            Authorization: `Bearer ${token}`,
        },
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load current user.')
        throw new Error(message)
    }

    return response.json()
}

export const updateCurrentUser = async (token, data) => {
    const response = await fetch(`${API_URL}/me`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to update profile.')
        throw new Error(message)
    }

    return response.json()
}