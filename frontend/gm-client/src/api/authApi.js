const API_URL = 'http://localhost:5262/api/Auth'

export const register = async (user) => {
    const response = await fetch(
        `${ API_URL }/register`,
{
    method: 'POST',
        headers: {
        'Content-Type': 'application/json',
            },
    body: JSON.stringify(user),
        }
    )

if (!response.ok) {
    const message = await response.text()
    throw new Error(
        message || 'Failed to create account.'
    )
}

return response.json()
}

export const login = async (credentials) => {
    const response = await fetch(
        `${API_URL}/login`,
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(credentials),
        }
    )

    if (!response.ok) {
        const message = await response.text()
        throw new Error(
            message || 'Invalid email or password.'
        )
    }

    return response.json()
}

export const getCurrentUser = async (token) => {
    const response = await fetch(
        `${API_URL}/me`,
        {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }
    )

    if (!response.ok) {
        throw new Error('Failed to load current user.')
    }

    return response.json()
}

export const updateCurrentUser = async (token, data) => {
    const response = await fetch(
        `${API_URL}/me`,
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(data),
        }
    )

    if (!response.ok) {
        const message = await response.text()
        throw new Error(
            message || 'Failed to update profile.'
        )
    }

    return response.json()
}