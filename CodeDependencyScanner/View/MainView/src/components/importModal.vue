<template>
  <modal class="import-modal" :opened="showModal" :close-on-click-away="false" @clickAwayModal="clickAwayModal">
    <p slot="header">Choose a .Net assembly</p>

    <div slot="content" class="ui padded grid">
        <div class="row column">
            <div class="four wide column">   
                <filechooser :path="path" :choose="choose">
                </filechooser>
            </div>
            <div class="twelve wide column">
                <transition  name="fade">
                    <progressdisplayer v-show="loading" :progress="progress">
                    </progressdisplayer>
                </transition>
            </div>
        </div>
    </div>

    <template slot="actions">
        <transition name="fade">
            <button-execute v-show="loading" :command="cancel" class="negative" :class="{'loading': cancelling}" @afterExecute="cancelling=true">
                <i class="cancel icon"></i>
                Cancel
            </button-execute>
        </transition>
        <button-command :command="compute" :class="[{'loading': loading}, 'primary']" @afterExecute="closeOnEndLoading=true">
            Analyze
        </button-command> 
    </template>

  </modal>

</template>

<script>
import modal from 'vue-semantic-modal'
import filechooser from './filechooser'
import buttonCommand from './buttoncommand'
import buttonExecute from './buttonExecute'
import progressdisplayer from './progressDisplayer'

const props = {
    showModal: {
        type: Boolean,
        default: false
    },
    choose: Object,
    path: String,
    compute: Object,
    cancel: Object,
    progress: Object,
    loading: Boolean
}

export default {
    components: {
        modal,
        filechooser,
        buttonCommand,
        buttonExecute,
        progressdisplayer
    },

    model : {
        prop: 'showModal',
        event: 'changed'
    },

    props,

    data () {
        return {
            closeOnEndLoading:false,
            cancelling: false
        }
    },

    methods: {
        requestClose () {
            this.$emit('changed', false)
            this.closeOnEndLoading = false
            this.cancelling = false
        },

        clickAwayModal () {
            if (this.loading) {
                return
            }
            this.requestClose()
        },

        afterCancel () {
            this.requestClose()
        }
    },

    watch: {
        loading (newValue) {
            if ((!newValue) && (this.closeOnEndLoading)) {
                this.requestClose()
            }
        }
    }
}
</script>

<style>
.fade-enter-active {
  transition: opacity .4s
}
.fade-leave-active {
  transition: opacity .9s
}
.fade-enter, .fade-leave-active {
  opacity: 0
}

.ui.dimmer.import-modal {
    top: 40px !important;
    height: calc(100% - 40px) !important;
    z-index: 8000;
}

top: 0em !important;

</style>