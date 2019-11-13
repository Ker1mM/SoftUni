function lockedProfile() {
    let profiles = document.getElementsByClassName('profile');

    Array
        .from(profiles)
        .map(x => x.getElementsByTagName('button')[0].addEventListener('click', function () {
            if (!isLocked(x)) {
                if (x.getElementsByTagName('button')[0].textContent === 'Show more') {
                    x.getElementsByTagName('div')[0].style.display = 'block';
                    x.getElementsByTagName('button')[0].textContent = 'Hide it';
                } else {
                    x.getElementsByTagName('div')[0].style.display = 'none';
                    x.getElementsByTagName('button')[0].textContent = 'Show more';
                }
            }
        }));

    function isLocked(profile) {
        if (profile.querySelector('[value="lock"]').checked === true) {
            return true;
        } else if (profile.querySelector('[value="unlock"]').checked === true) {
            return false;
        }
    }
}