<template>
  <div class="ui label assembly-detail">
    <div class="ui large label">
      {{assemblyName}}
    </div>
    <div class="detail">
      <div class="ui mini statistic">
        <div class="value">
          {{typesNumber}}
        </div>
        <div class="label">
          Types
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const props = {
  typesNumber: Number,
  assemblyName: String
}

export default {
  props
}
</script>

<style>
.ui.label.assembly-detail>.detail>.ui.statistic{
  margin-top: 0px;
}
</style>
