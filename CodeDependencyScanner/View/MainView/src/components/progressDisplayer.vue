<template>
  <div class="ui progress">
    <div class="bar animatedbar" :style="{width: porcentageText}">
      <div class="progress">{{porcentageText}}</div>
    </div>
    <div class="label">{{progress.Message}} ({{progress.Current}}/{{progress.Total}})</div>
  </div>
</template>

<script>

const props = {
  progress: Object
}

export default {
  props,

  computed: {
    porcentageText() {
      const value = (this.progress.Total===0)? 100 : Math.min(100, 100 * this.progress.Current/this.progress.Total);
      return Math.trunc(value) + '%';
    }
  }
}
</script>

<style>
.animatedbar{
  transition-duration: 200ms;
}
</style>
