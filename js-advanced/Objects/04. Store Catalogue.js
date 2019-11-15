function storeCatalogue(input) {
    let ordered = input.sort();

    let currentLetter = '';
    for (const product of ordered) {
        let productInfo = product.split(' : ');
        let productName = productInfo[0];
        let productPrice = productInfo[1];

        if (productName[0] !== currentLetter) {
            currentLetter = productName[0];
            console.log(currentLetter)
        }
        
        console.log(`  ${productName}: ${productPrice}`)
    }
}