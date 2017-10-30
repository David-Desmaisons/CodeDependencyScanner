<template>
  <div class="node-info-bar ui fluid card" v-if="node">
   
   <div class="content">
   
    <i class="right floated close icon" @click="close"></i>

    <div class="ui label big main-content">
      {{node.data.text}}
    </div>

    <div class="meta">
      <span class="date">{{namespace}}</span>
    </div>

    <div class="description node-info-bar-description">
      <div class="ui relaxed divided list">
        <div class="item" v-for="(value, key) in nodeLinks" :key="value">
          <div class="content">
            <div class="header">{{key}}</div>
                <a v-for="linked in value" class="ui blue image label wrapped-content" @click="select(linked)" :key="linked.data.text">{{linked.data.text}}</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {compareString} from '../infra/comparator'

let nodeTable = {}

const props = {
  node: Object,
  nodes: Object,
  links: Array,
  linksType: Array
}

function removeLast(array) {
  array.pop()
  return array
}

function sorting (a, b) {
  return compareString(a.data.text, b.data.text)
}

export default {
  props,
  methods: {
    linkName (id, inRelation) {
      const element = this.linksType.find(t => t.id === id)
      if (!element) {
        return null
      }
      return inRelation ? element.inName : element.outName
    },
    select (node) {
      this.$emit('select', node)
    },
    updateNodes (nodes) {
      if (!nodes){
        return
      }
      const leaves = nodes.leaves()
      leaves.forEach(node =>{
        nodeTable[node.data.id] = node
      })
    },
    close () {
      this.$emit('close')
    }
  },
  created () {
    this.updateNodes(this.nodes)
  },
  computed: {
    namespace () {
      var nameSpace = removeLast(removeLast(this.node.ancestors()).reverse())
      return nameSpace.map(el => el.data.text).reduce((tot,name) => !tot ? name : `${tot}.${name}`, null) 
    },
    nodeLinks () {
      const node = this.node
      const links = this.links
      const linksType = this.linksType 

      if (!node){
        return {}
      }

      const nameInBuilder = (linkType) => this.linkName(linkType, true)
      const nameOutBuilder = (linkType) => this.linkName(linkType, false)
      const inLinks = links.filter(l => l.target == node.data.id).map(l => ({linkType: nameOutBuilder(l.type), related: l.source}))
      const outLinks = links.filter(l => l.source == node.data.id).map(l => ({linkType: nameInBuilder(l.type), related: l.target}))
      const allLinks = [...inLinks, ...outLinks]
      const digestedLinks = {}
      allLinks.forEach(l => {
        const category = digestedLinks[l.linkType] || []
        category.push(nodeTable[l.related])
        digestedLinks[l.linkType] = category
      })
      Object.keys(digestedLinks).forEach ( key => { 
        digestedLinks[key].sort(sorting)
      })
      return digestedLinks
    }
  },
  watch: {
    nodes (newValue) {
      this.updateNodes(newValue)
    }
  }
}
</script>

<style>

.node-info-bar.ui.card > .content> .close.icon {
  cursor: pointer;
}

.node-info-bar {
  overflow-wrap: break-word;
}

.node-info-bar-description {
  max-height: 550px;
  overflow-y: auto;
  overflow-x: hidden;
}

.item .content .wrapped-content {
  max-width: 100%;
}

.ui.label.big.main-content {
  max-width: calc(100% - 30px) !important;
}


.node-info-bar-description::-webkit-scrollbar-track{
	-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
	border-radius: 4px;
	background-color: #F5F5F5;
}

.node-info-bar-description::-webkit-scrollbar{
	width: 8px;
	background-color: #F5F5F5;
}

.node-info-bar-description::-webkit-scrollbar-thumb{
	border-radius: 4px;
	-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
	background-color: #555;
}

.node-info-bar.card > .content {
  padding-right: 1px;
}
</style>
