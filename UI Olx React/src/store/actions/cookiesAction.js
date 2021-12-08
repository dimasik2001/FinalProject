import 'sfcookies'
import { bake_cookie, read_cookie } from 'sfcookies'
const key = 'favourites'
export const saveToFavourite = (ad) => {
    debugger
    let data = read_cookie(key);
    if(data.find(a => a.id == ad.id) == undefined)
    {
    data.push(ad)
    bake_cookie(key, data)
    }
    
}

export const deleteFromFavourite = (id) => {
    let data = read_cookie(key);
    data = data.filter((ad) => ad.id != id);
    bake_cookie(key, data)
}

export const getFromFavourite = () => {
    debugger
    let data = read_cookie(key);
    return data
}