sp_helpdb esbExceptionDb

backup log EsbExceptionDb to disk = N'd:\backupLogEsbExceptionDb'

dbcc shrinkfile ('EsbExceptionDb_Log',0,truncateonly)