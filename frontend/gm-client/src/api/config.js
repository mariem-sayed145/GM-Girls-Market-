export const API_BASE_URL = (import.meta.env.VITE_API_BASE_URL || 'http://localhost:5262').replace(/\/+$/, '')

export const getAuthHeaders = () => {
    const token = localStorage.getItem('gm_token')
    return token ? { Authorization: `Bearer ${token}` } : {}
}

export const getImageUrl = (url) => {
    if (!url) return '/images/logo.jpg'
    if (url.startsWith('http://') || url.startsWith('https://')) return url
    return `${API_BASE_URL}${url.startsWith('/') ? '' : '/'}${url}`
}

export const parseErrorMessage = async (response, fallbackMessage = 'An error occurred.') => {
    try {
        const data = await response.json()
        if (data.message) {
            if (data.errors) {
                const detailed = Object.values(data.errors).flat().join(' ')
                return `${data.message} ${detailed}`.trim()
            }
            return data.message
        }
        if (data.errors) {
            return Object.values(data.errors).flat().join(' ')
        }
    } catch {
        try {
            const text = await response.text()
            if (text) return text
        } catch {
            // fallback
        }
    }
    return fallbackMessage
}
