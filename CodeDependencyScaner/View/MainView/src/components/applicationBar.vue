 <template> 
    <div id="main-menu" class="ui fixed main inverted menu">
      <a class="item" @click="toogle">
        <i class="sidebar icon"></i>
      </a>

      <div class="right menu inverted">
        <div class="item">
          {{application}}
        </div>
        <simple-Button :command="window.Minimize" icon="minus"></simple-Button>
        <simple-Button :command="window.Normalize" icon="clone"></simple-Button>
        <simple-Button :command="window.Close" icon="close"></simple-Button>
    </div>
  </div>
</template>

<script>

import simpleButton from './simplebutton'

export default {
  components:{
    simpleButton
  },
  props:{
    application:{
      type: String,
      default:''
    },
    menuActive:{
      type: Boolean,
      default:false
    },
    window:{
      type: Object,
      required:true
    }
  },
  model: {
    prop: 'menuActive',
    event: 'menuActiveChanged'
  },
  methods:{
    toogle() {
      this.$emit('menuActiveChanged', !this.menuActive);
    }
  }
}

</script>

<style scoped="true">

#main-menu{
   -webkit-app-region: drag;
}

#main-menu a{
   -webkit-app-region: no-drag;
}
</style>