<template>
  <div class="root-app">
  
    <application-bar v-model="menuActive" :application="applicationName" :window="viewModel.Window">
    </application-bar>
  
    <div id="main-pushable" class="ui bottom attached segment pushable ">
  
      <application-lateral-menu :isOpen="menuActive">
      </application-lateral-menu>
  
      <div class="pusher">
  
        <div class="ui basic segment">
  
          <application-header>
          </application-header>
  
          <application-main-menu v-model="dataSelected" :node-options="nodeInformation.data" :assembly-name="assemblyName" @requestModal="requestModal">
          </application-main-menu>
  
          <div class="ui grid">
            <div class="row">
  
              <hierarchical-edge-bundling ref="graph" :data="graph" :links="links" :maxTextWidth="100" @clickOutsideGraph="resetNode" @mouseNodeOver="mouseNodeOver" @mouseNodeOut="mouseNodeOut" @mouseNodeClick="mouseNodeClick" @nodesComputed="onNodes" :node-text="'text'" :identifier="getId" :margin-x="0" :margin-y="0" class="twelve wide column tree">
              </hierarchical-edge-bundling>
  
              <div class="four wide column">
  
                <node-info :node="currentNode" :links="links" :nodes="nodes" :linksType="linksType" @select="onSelect" @close="resetNode">
                </node-info>
  
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  
    <import-modal v-model="showModal" :path="viewModel.Path" :choose="viewModel.ChooseFile" :compute="viewModel.Compute" :cancel="viewModel.Cancel" :loading="viewModel.Computing" :progress="viewModel.Progress">
    </import-modal>
  
    <application-notification ref="notification" @notified="onNotified">
    </application-notification>
  
  </div>
</template>

<script>
import applicationBar from './components/applicationBar'
import applicationLateralMenu from './components/applicationLeftMenu'
import applicationHeader from './components/applicationHeader'
import applicationMainMenu from './components/applicationMainMenu'
import applicationNotification from './components/applicationNotification'
import nodeInfo from './components/nodeInfo'
import importModal from './components/importModal'

import { hierarchicalEdgeBundling } from 'vued3tree'

const props={
    viewModel: Object,
    __window__: Object
};

export default {
  components: {
    applicationBar,
    applicationLateralMenu,
    applicationHeader,
    applicationNotification,
    nodeInfo,
    importModal,
    applicationMainMenu,
    hierarchicalEdgeBundling
  },
  data() {
    return {
      menuActive: false,
      applicationName: 'Code Analysis',
      currentNode: null,
      dataSelected: null,
      nodes: null,
      isPinned: false,
      showModal: false,
      animation: {
        enter: {
          opacity: [1, 0],
          translateX: [0, -300],
          scale: [1, 0.2]
        },
        leave: {
          opacity: 0,
          height: 0
        }
      }
    };
  },
  methods: {
    nameBuilder(item) {
      return item.data.text
    },
    changeCurrent(value) {
      this.dataSelected = null
      this.currentNode = value
    },
    getId(d) {
      return d.id
    },
    onSelect(node) {
      this.changeCurrent(node)
    },
    mouseNodeOver(event) {
      if (this.isPinned) {
        return
      }
      this.changeCurrent(event.element)
    },
    mouseNodeOut() {
      if (this.isPinned) {
        return
      }
      this.changeCurrent(null)
    },
    mouseNodeClick(event) {
      this.changeCurrent(event.element)
      this.isPinned = true
    },
    onNodes(nodes) {
      this.nodes = nodes
    },
    requestModal() {
      this.showModal = true
    },
    resetNode() {
      this.changeCurrent(null)
      this.isPinned = false
    },
    onNotified(notification) {
        this.showModal = false
    }
  },
  watch: {
    currentNode(value) {
      this.$refs['graph'].highlightedNode = value
    },
    dataSelected(value) {
      if (!value) {
        return
      }
      const node = this.nodeInformation.mapped[value.id].node
      this.currentNode = node
      this.isPinned = true
    },
    graph(value) {
      this.resetNode();
    },
    'viewModel.Message': function(value) {
      this.$refs['notification'].showMessage(value)
    }
  },
  computed: {
    nodeInformation() {
      if (!this.nodes) {
        return { leaves: [], data: [] }
      }
      const leaves = this.nodes.leaves()
      const data = leaves.map(n => n.data)
      const mapped = {}
      leaves.forEach(n => mapped[n.data.id] = {
        node: n,
        data: n.data
      })
      return { leaves, data, mapped }
    },
    links() {
      const { Graph } = this.viewModel
      return Graph !== null ? Graph.links : null
    },
    graph() {
      const { Graph } = this.viewModel
      return Graph !== null ? Graph.tree : null
    },
    assemblyName() {
      const { Graph } = this.viewModel
      return Graph !== null ? Graph.AssemblyName : null
    },
    linksType() {
      const { Graph } = this.viewModel
      return Graph !== null ? Graph.linksType : null
    }
  },
  name: 'app',
  props
}
</script>

<style>
@import '~dist/semantic.css';

.tree {
  height: 700px;
}

#main-menu {
  -webkit-app-region: drag;
}

#main-menu a {
  -webkit-app-region: no-drag;
}

#main-pushable {
  margin-top: 39px;
}

#main {
  height: 100%;
}

.root-app {
  height: 100%;
  overflow: hidden;
  -webkit-app-region: no-drag;
}
</style>
