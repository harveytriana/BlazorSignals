function getSiteName() {

    let result = window.location.href.replace(window.location.pathname, '');

    console.log("Site: ", result);

    return result;
}