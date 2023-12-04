/* -------------------------------------------------------------------------- */
/*                              Active search bar                             */
/* -------------------------------------------------------------------------- */
const inputSearch = document.getElementsByClassName("search");
const searchBar = document.getElementsByClassName("search-bar")

for (let item = 0; item < inputSearch.length; item++) {
    inputSearch[item].addEventListener("focus", () => {
        searchBar[item].classList.add("search-bar-active");
    })
    inputSearch[item].addEventListener("blur", () => {
        searchBar[item].classList.remove("search-bar-active");
    })
}

/* -------------------------------------------------------------------------- */
/*                             Open Secondary Menu                            */
/* -------------------------------------------------------------------------- */
const body = document.getElementsByClassName("body");
const iconMenu = document.getElementsByClassName("icon-menu");
const hamburgerMenu = document.getElementsByClassName("hamburger-menu");
const bgBlack = document.getElementsByClassName("bg-black");

for (let i = 0; i < iconMenu.length; i++) {
    iconMenu[i].addEventListener("click", () => {
        if (hamburgerMenu[0].classList.contains("menu-close")) {
            hamburgerMenu[0].classList.add("menu-open");
            hamburgerMenu[0].classList.remove("menu-close");
            bgBlack[0].style.display = "block";

        }
        else {
            hamburgerMenu[0].classList.remove("menu-open");
            hamburgerMenu[0].classList.add("menu-close");
            bgBlack[0].style.display = "none";
        }
    })
}
bgBlack[0].addEventListener("click", ()=>{
    if (hamburgerMenu[0].classList.contains("menu-close")) {
        hamburgerMenu[0].classList.add("menu-open");
        hamburgerMenu[0].classList.remove("menu-close");
        bgBlack[0].style.display = "block";

    }
    else {
        hamburgerMenu[0].classList.remove("menu-open");
        hamburgerMenu[0].classList.add("menu-close");
        bgBlack[0].style.display = "none";
    }
})

const iconCart = document.getElementsByClassName("icon-cart");
const cart = document.getElementsByClassName("cart-menu");
const bgBlackCart = document.getElementsByClassName("bg-active-cart");
for (let i = 0; i < iconCart.length; i++) {
    iconCart[i].addEventListener("click", () => {
        if (cart[0].classList.contains("menu-close-cart")) {
            cart[0].classList.add("menu-open-cart");
            cart[0].classList.remove("menu-close-cart");
            bgBlackCart[0].style.display = "block";

        }
        else {
            cart[0].classList.remove("menu-open-cart");
            cart[0].classList.add("menu-close-cart");
            bgBlackCart[0].style.display = "none";
        }
    })
}
bgBlackCart[0].addEventListener("click", ()=>{
    if (cart[0].classList.contains("menu-close-cart")) {
        cart[0].classList.add("menu-open-cart");
        cart[0].classList.remove("menu-close-cart");
        bgBlackCart[0].style.display = "block";

    }
    else {
        cart[0].classList.remove("menu-open-cart");
        cart[0].classList.add("menu-close-cart");
        bgBlackCart[0].style.display = "none";
    }
})
/* -------------------------------------------------------------------------- */
/*                                  Collapse                                  */
/* -------------------------------------------------------------------------- */
const collapse = document.getElementsByClassName("collapse");
const btnCollapse = document.getElementsByClassName("btn-collapse");

for (let i = 0; i < btnCollapse.length; i++) {
    btnCollapse[i].addEventListener("click", () => {
        if (collapse[i].classList.contains("collapse-hidden")) {
            collapse[i].classList.remove("collapse-hidden");
        }
        else {
            collapse[i].classList.add("collapse-hidden")
        }
    })
}

/* -------------------------------------------------------------------------- */
/*                              Submenu producto                              */
/* -------------------------------------------------------------------------- */
const subMenuItem = document.getElementsByClassName("btn-item-table");
const itemMenu = document.getElementsByClassName("manage-item");

for (let i = 0; i < subMenuItem.length; i++) {
    subMenuItem[i].addEventListener("click", ()=>{
        if(itemMenu[i].style.display == "flex"){
            itemMenu[i].style.display  = "none";
        }
        else{
            itemMenu[i].style.display  = "flex";
        }
    })
    
}

/* --------------------------------Carousel img----------------------------------- */
const showImagen = document.getElementsByClassName("show-imagen");
const btnImagen = document.getElementsByClassName("btn-imagen");
for(let i = 0; i < btnImagen.length; i++){
    btnImagen[i].addEventListener("mouseover", ()=>{
        for (let j = 0; j < showImagen.length; j++) {
            if (j === i) {
                // Muestra la imagen correspondiente al botón
                showImagen[j].style.display = "flex";
            } else {
                // Oculta las demás imágenes
                showImagen[j].style.display = "none";
            }
        }
    });
}