<template>
<div class="row">
  <div class="ui secondary  menu">
    <div class="item">
      <button class="ui positive button" @click="requestModal">
        <i class="upload icon"></i>
        Load an assembly
      </button>
    </div>
    
    <div class="item">
      <node-finder :value="value" :node-options="nodeOptions" :disabled="disableFinder" @input="input">
      </node-finder>
    </div>
      
    <div class="right menu">
      <div class="item">
        <transitionSlideUp>
          <assembly-detail v-show="assemblyName" :assembly-name="assemblyName" :typesNumber="nodeOptions.length">
          </assembly-detail>
        </transitionSlideUp>
      </div>
    </div>
  </div>
  </div>
</template>

<script>
import nodeFinder from './nodeFinder'
import assemblyDetail from './assemblyDetail'
import transitionSlideUp from './transitionSlideUp'


const props = {
  value: Object,
  nodeOptions: Array,
  assemblyName: String
}

export default {
  components: {
    nodeFinder,
    assemblyDetail,
    transitionSlideUp
  },

  props,

  methods: {
    requestModal() {
      this.$emit('requestModal')
    },

    input (evt) {
      this.$emit('input', evt)
    }
  },

  computed: {
      disableFinder () {
        return (!this.nodeOptions || this.nodeOptions.length === 0)
      }
    }
}
</script>

<style>
</style>
