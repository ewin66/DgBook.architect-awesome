



```csharp


 [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  //取消主键自增长，否则出现显示主键inseret 出错
        public override long Id { get; set; }

        public int BillNo { get; set; }

        [HttpPost("Create")]
        public string CreateMission(BillInfoDto input)
        { 
            input.Id = Snowflake.Instance().GetId(); //分布式ID作为主键
            var result = Mapper.Map<BillInfo>(input);

            var task = _BillInfoRepository.GetAll().
                Where(t => t.BillNo == input.BillNo && t.IsCandidate == false)
                .ToList().Count; //检查是否有同货票号且正在生效中信息
                                 //if (task <= 0)
                                 //{
            _BillInfoRepository.Insert(result);
            return "新增成功";
        }


{
  "id": 213,
  "billNo": 24,
  "isCandidate": false,
  "version": 0,
  "sendBranchID": "qweqwr",
  "billCheck": false,
  "billStateID": 0,
  "totaL_CHARGES": "23123",
  "billImgUrl": "123123"
}

```

