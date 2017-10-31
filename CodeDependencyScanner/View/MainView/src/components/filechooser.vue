<template>
  <div @click="choose.Execute()" class="ui icon button">
        <i class="file icon"></i>
        <span class="message-container">
          <transition-slide-up>
            <span class="message" :key="path">{{path===null? 'Choose File' : split(path)}}</span>
          </transition-slide-up>
        </span>
  </div>
</template>

<script>
import transitionSlideUp from './transitionSlideUp'

const props={
  choose: Object,
  path: String
};

export default {
  components:{
    transitionSlideUp
  },
  props,
  methods: {
    split(name){
      return name.substring(name.lastIndexOf('\\')+1);
    }
  }
}
</script>

<style>
.message-container {
  height: 1em;
}

.message-container>.message {
  transition: all .3s ease;
  display: inline-block;
  width: 150px;
/*  position: absolute;*/  
  overflow: hidden;
  text-overflow: ellipsis;
} 

.container {
  position: relative;
  z-index: 1;
  height: 100%;
}
</style>
