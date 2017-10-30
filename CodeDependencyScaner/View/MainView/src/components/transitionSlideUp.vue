 <template> 
  <transition name="slide-up" mode="out-in">
    <slot></slot>
  </transition>
</template>

<script>
export default {
}

</script>

<style>
.slide-up-enter {
  opacity: 0;
  transform: translate(0, 50%) ;
}

.slide-up-leave-active {
  opacity: 0;
  transform: translate(0, -50%);
}
</style>