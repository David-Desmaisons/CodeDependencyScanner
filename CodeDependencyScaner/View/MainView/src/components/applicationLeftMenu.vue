<template>
  <div class="ui inverted left inline vertical sidebar menu" :class="{'visible':isOpen}">
      <a class="item">
        <i class="share alternate icon"></i>
        Main
      </a>
      <a class="item">
        <i class="info circle icon"></i>
        About
      </a>
    </div>
</template>

<script>
export default {
  props:{
    isOpen: {
      type: Boolean,
      requires:true
    }
  },
  data(){
    return {
      applicationName: '.Net code analizer'
    }
  }
}
</script>

<style>
</style>
