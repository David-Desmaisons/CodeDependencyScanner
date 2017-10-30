<template>
  <div class="ui button" :class="{'disabled': !canExecute}" @click="execute">   
    <slot></slot>  
  </div>
</template>
<script>
import comandMixin from 'neutronium-vue-command-mixin'

export default {
  mixins:[comandMixin]
}
</script>

<style>
</style>
