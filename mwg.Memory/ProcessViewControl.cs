#define DESIGN

using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Diag=System.Diagnostics;

#if DESIGN
using BaseClass=System.Windows.Forms.UserControl;
#else
using BaseClass=System.Windows.Forms.Control;
#endif

namespace mwg.InterProcess{
	public class ProcessView:BaseClass{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Splitter splitter1;
	
		public ProcessView(){
			this.InitializeComponent();
		}

		private void InitializeComponent(){
			this.treeView1=new System.Windows.Forms.TreeView();
			this.splitter1=new System.Windows.Forms.Splitter();
			this.propertyGrid1=new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock=System.Windows.Forms.DockStyle.Left;
			this.treeView1.Location=new System.Drawing.Point(0,0);
			this.treeView1.Name="treeView1";
			this.treeView1.Size=new System.Drawing.Size(162,344);
			this.treeView1.TabIndex=0;
			this.treeView1.BeforeExpand+=new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			this.treeView1.AfterSelect+=new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location=new System.Drawing.Point(162,0);
			this.splitter1.Name="splitter1";
			this.splitter1.Size=new System.Drawing.Size(3,344);
			this.splitter1.TabIndex=1;
			this.splitter1.TabStop=false;
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock=System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location=new System.Drawing.Point(165,0);
			this.propertyGrid1.Name="propertyGrid1";
			this.propertyGrid1.Size=new System.Drawing.Size(239,344);
			this.propertyGrid1.TabIndex=2;
			// 
			// ProcessView
			// 
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
			this.Name="ProcessView";
			this.Size=new System.Drawing.Size(404,344);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// 観察の対象のプロセスをコントロール上に追加します。
		/// </summary>
		/// <param name="mem">観察対象のプロセスのメモリ空間を示す ProcessMemory インスタンスを指定します。</param>
		public void SetProcess(ProcessMemory mem){
			if(!mem.Available)return;

			Forms::TreeNode processNode=new Forms::TreeNode(mem.Process.ProcessName);
			foreach(Diag::ProcessModule mod in mem.Process.Modules){
				Module module=new Module(mem,mod);
				processNode.Nodes.Add(this.CreateModuleNode(module));
			}
			processNode.Expand();
			treeView1.Nodes.Add(processNode);
		}

		private const string BEFORE_EXPAND="<void>";
		private Forms::TreeNode CreateModuleNode(Module module){
			Forms::TreeNode node=new Forms::TreeNode(module.ClrModule.ModuleName);
			node.Tag=module;
			node.Nodes.Add(BEFORE_EXPAND);
			return node;
		}

		private void treeView1_AfterSelect(object sender,Forms::TreeViewEventArgs e){
			Module module=e.Node.Tag as Module;
			if(module!=null){
				this.propertyGrid1.SelectedObject=module;
				return;
			}

			ImageImportDirectory import=e.Node.Tag as ImageImportDirectory;
			if(import!=null){
				this.propertyGrid1.SelectedObject=import;
				return;
			}

			ImageImportDirectory.ImportModule imod=e.Node.Tag as ImageImportDirectory.ImportModule;
			if(imod!=null){
				this.propertyGrid1.SelectedObject=imod;
				return;
			}
		}

		private void treeView1_BeforeExpand(object sender,Forms::TreeViewCancelEventArgs e) {
			Module module=e.Node.Tag as Module;
			if(module!=null){
				if(e.Node.Nodes.Count>0&&e.Node.Nodes[0].Text!=BEFORE_EXPAND)return;

				e.Node.Nodes.Clear();
				ImageImportDirectory itable
					=(ImageImportDirectory)module.Directories[mwg.Win32.IMAGE.DIRECTORY_ENTRY.IMPORT];
				Forms::TreeNode node=new Forms::TreeNode("ImportTable");
				node.Tag=itable;
				node.Nodes.Add(BEFORE_EXPAND);
				e.Node.Nodes.Add(node);
				return;
			}

			ImageImportDirectory import=e.Node.Tag as ImageImportDirectory;
			if(import!=null){
				if(e.Node.Nodes.Count>0&&e.Node.Nodes[0].Text!=BEFORE_EXPAND)return;

				e.Node.Nodes.Clear();
				foreach(ImageImportDirectory.ImportModule imod in import){
					Forms::TreeNode child=new Forms::TreeNode(imod.Name);
					child.Tag=imod;
					e.Node.Nodes.Add(child);
				}
				return;
			}
		}
	}
}