
require 'albacore'
require 'fileutils'

def ReCreate(path)
  if Dir.exist?(path)   
    FileUtils.remove_dir(path)  
  end  
  FileUtils.mkdir(path)
end

def CreateDirIfNotExists(path)
  if !Dir.exist?(path)
    FileUtils.mkdir(path)
  end
end


def DeleteFileIfExists(path)
  if File.exist?(path)
    File.delete(path)
  end
end


dependingOn =  (ENV['depending_on'] ||= ''). split(',')      # What file grab from folder Lib to publish
Environment = { :solution_name =>  ENV['solution_name'] }

Folder =
    {
        :src => ENV['src'] ||= 'src',
        :config => ENV['config'] ||= File.join('config' , 'ci'),
        :deploy => 'deploy',
        :live =>  File.join('deploy' , 'Live'),
        :dev =>  File.join( 'deploy' , 'Dev'),		
        :prePublish => 'Deploy/Dev/_PublishedWebsites/UI/*', # relative path to folder with publish site.
        :lib => ENV['lib'] ||= 'src/Lib/*',
	    :mspecResult =>  File.join('deploy','MspecReport'),
	    :iis => ENV['iis_folder']
    }

Files  =
    {
    :packageLive =>  ENV['path_package_live'] ||=  "../#{Environment[:solution_name]}.zip",
    :sln =>  ENV['path_sln'] ||= Environment[:solution_name] +  '.sln', 
    :mspecRunner =>  ENV['path_mspec_runner'] ||= 'tools/m-spec/mspec-clr4.exe', 
    :unitTestDll =>   ENV['path_unit_test_dll'] ||= Folder[:dev] + '/UnitTest.dll'  ,
	:dbConfig => ENV['path_db_config'] ||= Folder[:config] + '/'  + 'db.config',
    :appConfig => ENV['path_app_config'] ||= Folder[:config] + '/'  + 'appweb.config',
    :appOffline => ENV['path_app_offline'] ||= 'App_Offline.htm',
}

task :cleanup do
  ReCreate(Folder[:deploy])
  ReCreate(Folder[:iis])   
  ReCreate(Folder[:live])
  ReCreate(Folder[:mspecResult])
      
  FileUtils.cp_r(File.expand_path(Files[:dbConfig]),Folder[:src] + '/UI',:verbose => true)
  FileUtils.cp_r(File.expand_path(Files[:appConfig]),Folder[:src] + '/UI',:verbose => true)
  FileUtils.cp_r(File.expand_path(Files[:dbConfig]),Folder[:src] + '/UnitTest',:verbose => true)  
  FileUtils.cp_r(File.expand_path(Files[:appOffline]),Folder[:iis],:verbose => true)   
end


desc 'Increment assembly UI version'
assemblyinfo :assemblyinfoUI do |asm|
  asm.version = ENV['build_number'] 
  asm.file_version = ENV['build_number']
  asm.company_name = ENV['company_name'] ||= 'Incoding Software'
  asm.product_name = ENV['product_name'] ||= Environment[:solution_name]
  asm.copyright = ENV['copyright'] ||=  'Incoding Software'
  asm.output_file =  Folder[:src] +"/UI/Properties/AssemblyInfo.cs"
end

desc 'Increment assembly Domain version'
assemblyinfo :assemblyinfoDomain do |asm|
  asm.version = ENV['build_number'] 
  asm.file_version = ENV['build_number']
  asm.company_name = ENV['company_name'] ||= 'Incoding Software'
  asm.product_name = ENV['product_name'] ||= Environment[:solution_name]
  asm.copyright = ENV['copyright'] ||=  'Incoding Software'
  asm.output_file =  Folder[:src] +"/Domain/Properties/AssemblyInfo.cs"
end

desc    'Clean and Build solution'
msbuild :build =>[:assemblyinfoUI,:assemblyinfoDomain] do |msb|
  msb.properties :configuration => :Release, :OutputPath => :"../../#{Folder[:dev]}"
  msb.targets :Clean,:Build
  msb.solution = File.join(Folder[:src],Files[:sln])
end

desc 'Execute integrated test'
mspec do |mspec|
  FileUtils.cp_r(File.expand_path(Files[:dbConfig]),Folder[:dev],:verbose => true)  
  mspec.command = Files[:mspecRunner]
  mspec.assemblies Files[:unitTestDll]
  mspec.html_output = Folder[:mspecResult]
  mspec.results_path = Folder[:mspecResult]
end

desc 'Copy all dependency from folder lib to target folder'
task :resolveDependency do
  Dir[Folder[:lib]].each do |f|
    baseName = File.basename(f)
	puts baseName
    if dependingOn.include?(baseName)
      folderBinInLive = "#{Folder[:live]}/bin"      
      FileUtils.cp_r(f, folderBinInLive,:verbose => true);
    end
  end
end

desc 'Copy files to publish folder from pre publish folder'
task :publish =>[:resolveDependency]  do
 Dir[Folder[:prePublish]].each do |f|
   FileUtils.cp_r(File.expand_path(f),Folder[:live],:verbose => true)
 end
 FileUtils.cp_r(File.expand_path(Files[:dbConfig]),Folder[:live],:verbose => true)

end

desc 'Package artifacts to TeamCity'
zip:packageArtifacts do |zip|
  zip.directories_to_zip Folder[:live]
  zip.output_file = Files[:packageLive]
end

desc 'Copy file to iis web site directory'
task:publishToIss do   
  FileUtils.rm_r(Dir[Folder[:iis] + '/*'],:verbose => true)
  FileUtils.cp_r(Dir[Folder[:live] +'/*'], Folder[:iis],:verbose => true)  
end

task :default => [:cleanup,:build,:publish,:packageArtifacts,:publishToIss] do
end





