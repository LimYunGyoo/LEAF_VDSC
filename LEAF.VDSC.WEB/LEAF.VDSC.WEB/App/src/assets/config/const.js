// constsPlugin.js
const CONSTANT = {
    SERVICE_URL: 'test'
}

CONSTANT.install = function (Vue, options) {
    Vue.prototype.$getConst = (key) => {
        return CONSTANT[key]
    }
}

export default CONSTANT