import Vue from 'vue'
import App from './App.vue'
import rawVm from '../data/simple'
import {install} from './install'
import { createVM } from 'neutronium-vm-loader'

const vm = createVM(rawVm);

install(Vue)
new Vue({
    components:{
        App
    },
    el: '#main',
    data: vm
})