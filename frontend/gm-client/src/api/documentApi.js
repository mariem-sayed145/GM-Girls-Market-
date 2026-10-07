import { API_BASE_URL, getAuthHeaders, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/Documents`

export const uploadDocument = async (file) => {
    const formData = new FormData()
    formData.append('file', file)

    const response = await fetch(API_URL, {
        method: 'POST',
        headers: getAuthHeaders(),
        body: formData,
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to upload document.')
        throw new Error(message)
    }

    return response.json()
}

export const getMyDocuments = async () => {
    const response = await fetch(`${API_URL}/my`, {
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to load documents.')
        throw new Error(message)
    }

    return response.json()
}

export const deleteDocument = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: getAuthHeaders(),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to delete document.')
        throw new Error(message)
    }
}
