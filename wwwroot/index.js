const adjectives = [
    "fluffy","tiny","sleepy","happy","snuggly","sparkly",
    "cuddly","soft","puffy","bouncy","cozy","squishy"
];

const cuteThings = [
    "bunny","panda","kitten","puppy","otter","hamster","duckling","koala","penguin","hedgehog",
    "seal","ferret","chipmunk","squirrel","fawn","lamb","calf","pony","chick","turtle",
    "frog","gecko","parrot","sparrow","owl","dove","goldfish","guppy","bee","ladybug",

    "apple","pear","peach","plum","cherry","strawberry","raspberry","blueberry","blackberry","cranberry",
    "banana","mango","papaya","pineapple","kiwi","melon","watermelon","cantaloupe","grape","apricot",
    "fig","pomegranate","persimmon","lychee","dragonfruit","passionfruit","guava","tangerine","clementine","orange",

    "carrot","pea","pumpkin","broccoli","lettuce","cucumber","corn","radish","tomato","onion",
    "garlic","potato","sweetpotato","zucchini","eggplant","pepper","bellpepper","chili","spinach","kale",
    "cabbage","cauliflower","asparagus","beet","turnip","parsnip","leek","scallion","bokchoy","celery",

    "avocado","olive","artichoke","okra","yam","butternut","acorn","ginger","wasabi","daikon",
    "lotusroot","shallot","endive","arugula","watercress","chard","mustardgreen","snappea","snowpea","edamame"
];

const sizeRange = document.getElementById("size-range");
const sizeDisplay = document.getElementById("size-display");
const salDisplay = document.getElementById("salt");
const passwordDisplay = document.getElementById("password");

sizeRange.addEventListener("input", () => {
    sizeDisplay.textContent = sizeRange.value;
    GenerateCredentials()
});

salDisplay.addEventListener("input", ()=>{
    GenerateCredentials()
});
passwordDisplay.addEventListener("input", ()=>{
    GenerateCredentials()
});
var domain = "";
fetch("/api/Mail/GetDomain")
    .then(res => res.json())
    .then(data => {
        domain = data.domain;
    });

function togglePassword(id) {
    let passwordField = document.getElementById(id);
    passwordField.type = passwordField.type === "password" ? "text" : "password";
}

async function GeneratePassword(password, salt) {
    const size = document.getElementById("size-range").value;
    const encoder = new TextEncoder();
    const passwordBytes = encoder.encode(password + salt + size);

    const hashBuffer = await crypto.subtle.digest('SHA-256', passwordBytes);
    const hashArray = new Uint8Array(hashBuffer);

    const base64Hash = btoa(String.fromCharCode.apply(null, hashArray));
    return base64Hash.substring(0,size);
}
function capitalizeFirstLetter(str) {
    if (!str) return str; 
    return str.charAt(0).toUpperCase() + str.slice(1);
}

async function GenerateMail() {
    let name = document.getElementById('name').value;
    const mailData = {
        Name: name 
    };

    fetch("/api/Mail", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(mailData)
    })
        .then(response => {
            if (response.ok) {
                console.log("Mail creado correctamente");
            } else {
                console.error("Error al crear mail:", response.statusText);
            }
        })
        .catch(error => {
            console.error("Error en la petición:", error);
        });
}

function GenerateNameFromPassword(base64String) {
    var name = getValueFromBase64(base64String, cuteThings);
    var adjetive = getValueFromBase64(base64String, adjectives);
    return capitalizeFirstLetter(adjetive)+capitalizeFirstLetter(name);
}

function getValueFromBase64(base64, array) {
    
    const decoded = atob(base64); 
    let num = 0n;

    for (let i = 0; i < decoded.length; i++) {
        num = num * 256n + BigInt(decoded.charCodeAt(i));
    }
    
    const index = Number(num % BigInt(array.length));
    return array[index];
}

async function GenerateCredentials(){
    const masterPassword = document.getElementById('password').value;
    const salt = document.getElementById('salt').value;
    const nameInput = document.getElementById('name');
    const mailInput = document.getElementById('mail');
    
    if (!masterPassword) {
        return;
    }

    if (!salt) {
        return;
    }
    
    let password = await GeneratePassword(masterPassword, salt);
    
    document.getElementById('passwordHashed').value = password;

    let name = GenerateNameFromPassword(password)

    nameInput.value = name;
    mailInput.value = name+'@'+domain;
}