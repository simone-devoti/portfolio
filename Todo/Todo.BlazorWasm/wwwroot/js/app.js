function getFromLocalStorage(key) {
    return window.localStorage.getItem(key);
}

function setToLocalStorage(key, value) {
    window.localStorage.setItem(key, value);
}