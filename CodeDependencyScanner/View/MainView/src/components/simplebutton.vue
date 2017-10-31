<template>
  <a class="item" @click="execute">
    <i :class="icon" class="icon" ></i>
  </a>
</template>
<script>

import mixin from 'neutronium-vue-simple-command-mixin'

export default {
  mixins:[mixin],
  props:{
    icon:{
      type: String,
      required:true
    }
  }

}
</script>

<style>
</style>
