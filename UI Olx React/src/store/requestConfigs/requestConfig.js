let userDataJson= localStorage.getItem('userData');
        let userData = JSON.parse(userDataJson)
        const accessToken = userData.accessToken;
export const authWithContentType = {
    headers: {
        "Authorization": `Bearer ${accessToken}`,
        "Content-Type": 'multipart/form-data'
    }  
}

export const auth = {
    headers: {
        "Authorization": `Bearer ${accessToken}`
    }
}