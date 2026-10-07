import { API_BASE_URL, parseErrorMessage } from './config'

const API_URL = `${API_BASE_URL}/api/Contact`

export const sendContactInquiry = async (data) => {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })

    if (!response.ok) {
        const message = await parseErrorMessage(response, 'Failed to send your message.')
        throw new Error(message)
    }

    return response.json()
}
