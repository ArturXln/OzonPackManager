namespace OzonPackManager
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bLoadOrders = new System.Windows.Forms.Button();
            this.dtShippingDate = new System.Windows.Forms.DateTimePicker();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.lbOrderID = new System.Windows.Forms.Label();
            this.lbProductName = new System.Windows.Forms.Label();
            this.lbSKU = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.lCountProducts = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lStock = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbPrintLabel = new System.Windows.Forms.CheckBox();
            this.lOrdersCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bGetScanner = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.SuspendLayout();
            // 
            // bLoadOrders
            // 
            this.bLoadOrders.AutoSize = true;
            this.bLoadOrders.Location = new System.Drawing.Point(12, 512);
            this.bLoadOrders.Name = "bLoadOrders";
            this.bLoadOrders.Size = new System.Drawing.Size(151, 23);
            this.bLoadOrders.TabIndex = 0;
            this.bLoadOrders.Text = "Загрузить заказы по дате";
            this.bLoadOrders.UseVisualStyleBackColor = true;
            this.bLoadOrders.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtShippingDate
            // 
            this.dtShippingDate.Location = new System.Drawing.Point(12, 486);
            this.dtShippingDate.Name = "dtShippingDate";
            this.dtShippingDate.Size = new System.Drawing.Size(127, 20);
            this.dtShippingDate.TabIndex = 1;
            // 
            // picProduct
            // 
            this.picProduct.Image = ((System.Drawing.Image)(resources.GetObject("picProduct.Image")));
            this.picProduct.InitialImage = null;
            this.picProduct.Location = new System.Drawing.Point(12, 12);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(375, 406);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProduct.TabIndex = 2;
            this.picProduct.TabStop = false;
            // 
            // lbOrderID
            // 
            this.lbOrderID.AutoSize = true;
            this.lbOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbOrderID.Location = new System.Drawing.Point(431, 12);
            this.lbOrderID.Name = "lbOrderID";
            this.lbOrderID.Size = new System.Drawing.Size(333, 63);
            this.lbOrderID.TabIndex = 3;
            this.lbOrderID.Text = "Список пуст";
            // 
            // lbProductName
            // 
            this.lbProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbProductName.Location = new System.Drawing.Point(4, 428);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(917, 46);
            this.lbProductName.TabIndex = 4;
            this.lbProductName.Text = "-";
            // 
            // lbSKU
            // 
            this.lbSKU.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSKU.Location = new System.Drawing.Point(434, 170);
            this.lbSKU.Name = "lbSKU";
            this.lbSKU.Size = new System.Drawing.Size(377, 46);
            this.lbSKU.TabIndex = 5;
            this.lbSKU.Text = "SKU";
            this.lbSKU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(435, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(303, 39);
            this.label2.TabIndex = 6;
            this.label2.Text = "Товаров в заказе:";
            // 
            // picBarcode
            // 
            this.picBarcode.Location = new System.Drawing.Point(440, 213);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(371, 152);
            this.picBarcode.TabIndex = 7;
            this.picBarcode.TabStop = false;
            // 
            // lCountProducts
            // 
            this.lCountProducts.AutoSize = true;
            this.lCountProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lCountProducts.Location = new System.Drawing.Point(779, 81);
            this.lCountProducts.Name = "lCountProducts";
            this.lCountProducts.Size = new System.Drawing.Size(36, 39);
            this.lCountProducts.TabIndex = 8;
            this.lCountProducts.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(435, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "На складе:";
            // 
            // lStock
            // 
            this.lStock.AutoSize = true;
            this.lStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStock.Location = new System.Drawing.Point(779, 121);
            this.lStock.Name = "lStock";
            this.lStock.Size = new System.Drawing.Size(36, 39);
            this.lStock.TabIndex = 10;
            this.lStock.Text = "0";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(187, 512);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Собрать товар";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cbPrintLabel
            // 
            this.cbPrintLabel.AutoSize = true;
            this.cbPrintLabel.Checked = true;
            this.cbPrintLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrintLabel.Location = new System.Drawing.Point(187, 489);
            this.cbPrintLabel.Name = "cbPrintLabel";
            this.cbPrintLabel.Size = new System.Drawing.Size(199, 17);
            this.cbPrintLabel.TabIndex = 12;
            this.cbPrintLabel.Text = "Печать этикетки при оформлении";
            this.cbPrintLabel.UseVisualStyleBackColor = true;
            // 
            // lOrdersCount
            // 
            this.lOrdersCount.AutoSize = true;
            this.lOrdersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOrdersCount.Location = new System.Drawing.Point(779, 366);
            this.lOrdersCount.Name = "lOrdersCount";
            this.lOrdersCount.Size = new System.Drawing.Size(36, 39);
            this.lOrdersCount.TabIndex = 14;
            this.lOrdersCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(434, 366);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(300, 39);
            this.label3.TabIndex = 13;
            this.label3.Text = "Очередь заказов:";
            // 
            // bGetScanner
            // 
            this.bGetScanner.AutoSize = true;
            this.bGetScanner.Location = new System.Drawing.Point(751, 512);
            this.bGetScanner.Name = "bGetScanner";
            this.bGetScanner.Size = new System.Drawing.Size(170, 23);
            this.bGetScanner.TabIndex = 15;
            this.bGetScanner.Text = "Подключить сканер";
            this.bGetScanner.UseVisualStyleBackColor = true;
            this.bGetScanner.Click += new System.EventHandler(this.bGetScanner_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(933, 564);
            this.Controls.Add(this.bGetScanner);
            this.Controls.Add(this.lOrdersCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPrintLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lStock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lCountProducts);
            this.Controls.Add(this.picBarcode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbSKU);
            this.Controls.Add(this.lbProductName);
            this.Controls.Add(this.lbOrderID);
            this.Controls.Add(this.picProduct);
            this.Controls.Add(this.dtShippingDate);
            this.Controls.Add(this.bLoadOrders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Сборка заказов OZON";
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLoadOrders;
        private System.Windows.Forms.DateTimePicker dtShippingDate;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lbOrderID;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Label lbSKU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Label lCountProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lStock;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbPrintLabel;
        private System.Windows.Forms.Label lOrdersCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bGetScanner;
    }
}

